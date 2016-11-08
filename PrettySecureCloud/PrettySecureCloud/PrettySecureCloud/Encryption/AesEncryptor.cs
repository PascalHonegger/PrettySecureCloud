using PCLCrypto;

namespace PrettySecureCloud.Encryption
{
	/// <summary>
	/// The Encryptor
	/// </summary>
	public class AesEncryptor : IByteEncryptor
	{
		/// <summary>
		/// Encrypts given bytes using a symmetric alogrithm
		/// </summary>
		/// <param name="data">data to encrypt</param>
		///^<param name="key">key to encryptr</param>
		/// <returns>Encrypted data</returns> 
		public byte[] Encrypt(byte[] data, byte[] key)
		{
			ISymmetricKeyAlgorithmProvider aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
			ICryptographicKey symetricKey = aes.CreateSymmetricKey(key);
			var bytes = WinRTCrypto.CryptographicEngine.Encrypt(symetricKey, data);
			return bytes;
		}

		/// <summary>
		/// Decrypts given bytes using a symmetric alogrithm
		/// </summary>
		/// <param name="data">data to decrypt</param>
		///<param name="key">key to decrypt</param>
		/// <returns>Decrypted data</returns>
		public byte[] Decrypt(byte[] data, byte[] key)
		{
			ISymmetricKeyAlgorithmProvider aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
			ICryptographicKey symetricKey = aes.CreateSymmetricKey(key);
			var bytes = WinRTCrypto.CryptographicEngine.Decrypt(symetricKey, data);
			return bytes;
		}
	}
}