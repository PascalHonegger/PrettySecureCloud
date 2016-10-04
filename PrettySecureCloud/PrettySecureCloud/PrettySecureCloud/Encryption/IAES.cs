namespace PrettySecureCloud.Encryption
{
	internal interface IAES
	{

		/// <summary>    
		/// Decrypts given bytes using symmetric alogrithm AES    
		/// </summary>    
		/// <param name="data">data to decrypt</param>    
		///<param name="key">key to decrypt</param>  
		/// <returns></returns>    
		byte[] DecryptAes(byte[] data, byte[] key);

		/// <summary>    
		/// Encrypts given bytes using symmetric alogrithm AES    
		/// </summary>    
		/// <param name="data">data to encrypt</param>    
		///^<param name="key">key to encryptr</param>  
		/// <returns></returns> 
		byte[] EncryptAes(byte[] data, byte[] key);
	}
}