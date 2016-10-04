using System.Text;
using PCLCrypto;

namespace PrettySecureCloud.Encryption
{
	public class AES : IAES
	{
		/// <summary>    
		/// Encrypts given bytes using symmetric alogrithm AES    
		/// </summary>    
		/// <param name="data">data to encrypt</param>    
		///^<param name="key">key to encryptr</param>  
		/// <returns></returns>  
		public byte[] EncryptAes(byte[] data, byte[] key)
		{
			ISymmetricKeyAlgorithmProvider aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
			ICryptographicKey symetricKey = aes.CreateSymmetricKey(key);
			var bytes = WinRTCrypto.CryptographicEngine.Encrypt(symetricKey, data);
			return bytes;
		}

		/// <summary>    
		/// Decrypts given bytes using symmetric alogrithm AES    
		/// </summary>    
		/// <param name="data">data to decrypt</param>    
		///<param name="key">key to decrypt</param>  
		/// <returns></returns>    
		public byte[] DecryptAes(byte[] data, byte[] key)
		{

			ISymmetricKeyAlgorithmProvider aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
			ICryptographicKey symetricKey = aes.CreateSymmetricKey(key);
			var bytes = WinRTCrypto.CryptographicEngine.Decrypt(symetricKey, data);
			return bytes;
		}
	}
}