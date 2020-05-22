namespace TestCaseManager.Core.CryptoService.UserRelated
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Macs;
    using Org.BouncyCastle.Crypto.Modes;
    using Org.BouncyCastle.Crypto.Paddings;
    using Org.BouncyCastle.Crypto.Parameters;

    // Taken from stackoverflow
    public class CredentialsCryptoService<TBlockCipher, TDigest>
        where TBlockCipher : IBlockCipher, new()
        where TDigest : IDigest, new()
    {
        private readonly Encoding _encoding;

        private readonly byte[] _key;
        private IBlockCipher _blockCipher;

        private BufferedBlockCipher _cipher;

        private HMac _mac;

        public CredentialsCryptoService(Encoding encoding, byte[] key, byte[] macKey)
        {
            _encoding = encoding;
            _key = key;
            Init(key, macKey, new Pkcs7Padding());
        }

        public CredentialsCryptoService(Encoding encoding, byte[] key, byte[] macKey, IBlockCipherPadding padding)
        {
            _encoding = encoding;
            _key = key;
            Init(key, macKey, padding);
        }

        private void Init(byte[] key, byte[] macKey, IBlockCipherPadding padding)
        {
            _blockCipher = new CbcBlockCipher(new TBlockCipher());
            _cipher = new PaddedBufferedBlockCipher(_blockCipher, padding);
            _mac = new HMac(new TDigest());
            _mac.Init(new KeyParameter(macKey));
        }

        public string Encrypt(string plain)
        {
            return Convert.ToBase64String(EncryptBytes(plain));
        }

        public byte[] EncryptBytes(string plain)
        {
            var input = _encoding.GetBytes(plain);

            var iv = GenerateIv();

            var cipher = BouncyCastleCrypto(true, input, new ParametersWithIV(new KeyParameter(_key), iv));
            var message = CombineArrays(iv, cipher);

            _mac.Reset();
            _mac.BlockUpdate(message, 0, message.Length);
            var digest = new byte[_mac.GetUnderlyingDigest().GetDigestSize()];
            _mac.DoFinal(digest, 0);

            var result = CombineArrays(digest, message);
            return result;
        }

        public byte[] DecryptBytes(byte[] bytes)
        {
            // split the digest into component parts
            var digest = new byte[_mac.GetUnderlyingDigest().GetDigestSize()];
            var message = new byte[bytes.Length - digest.Length];
            var iv = new byte[_blockCipher.GetBlockSize()];
            var cipher = new byte[message.Length - iv.Length];

            Buffer.BlockCopy(bytes, 0, digest, 0, digest.Length);
            Buffer.BlockCopy(bytes, digest.Length, message, 0, message.Length);
            if (!IsValidHMac(digest, message)) throw new CryptoException();

            Buffer.BlockCopy(message, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(message, iv.Length, cipher, 0, cipher.Length);

            var result = BouncyCastleCrypto(false, cipher, new ParametersWithIV(new KeyParameter(_key), iv));
            return result;
        }

        public string Decrypt(byte[] bytes)
        {
            return _encoding.GetString(DecryptBytes(bytes));
        }

        public string Decrypt(string cipher)
        {
            return Decrypt(Convert.FromBase64String(cipher));
        }

        private bool IsValidHMac(byte[] digest, byte[] message)
        {
            _mac.Reset();
            _mac.BlockUpdate(message, 0, message.Length);
            var computed = new byte[_mac.GetUnderlyingDigest().GetDigestSize()];
            _mac.DoFinal(computed, 0);

            return AreEqual(digest, computed);
        }

        private static bool AreEqual(byte[] digest, byte[] computed)
        {
            if (digest.Length != computed.Length) return false;

            var result = 0;
            for (var i = 0; i < digest.Length; i++) result |= digest[i] ^ computed[i];

            return result == 0;
        }

        private byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, ICipherParameters parameters)
        {
            _cipher.Init(forEncrypt, parameters);

            return _cipher.DoFinal(input);
        }

        private byte[] GenerateIv()
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                // 1st block
                var result = new byte[_blockCipher.GetBlockSize()];
                provider.GetBytes(result);

                return result;
            }
        }

        private static byte[] CombineArrays(byte[] source1, byte[] source2)
        {
            var result = new byte[source1.Length + source2.Length];
            Buffer.BlockCopy(source1, 0, result, 0, source1.Length);
            Buffer.BlockCopy(source2, 0, result, source1.Length, source2.Length);

            return result;
        }
    }
}