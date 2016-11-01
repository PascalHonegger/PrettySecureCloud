namespace PrettySecureCloud.Encryption
{
	public interface IByteEncryptor
	{
		/// <summary>
		/// Decrypts given bytes using a symmetric alogrithm
		/// </summary>
		/// <param name="data">data to decrypt</param>
		///<param name="key">key to decrypt</param>
		/// <returns>Decrypted data</returns>
		byte[] Decrypt(byte[] data, byte[] key);

		/// <summary>
		/// Encrypts given bytes using a symmetric alogrithm
		/// </summary>
		/// <param name="data">data to encrypt</param>
		///^<param name="key">key to encryptr</param>
		/// <returns>Encrypted data</returns> 
		byte[] Encrypt(byte[] data, byte[] key);
	}
}