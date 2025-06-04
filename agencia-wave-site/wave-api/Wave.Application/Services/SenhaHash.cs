using System;
using System.Security.Cryptography;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        // Gera um salt aleatório de 16 bytes (128 bits)
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        // Aplica PBKDF2 com SHA-256, 100.000 iterações
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32); // 32 bytes = 256 bits

        // Concatena salt + hash
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);

        // Retorna o hash completo em Base64
        return Convert.ToBase64String(hashBytes);
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        // Extrai o salt do início
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        // Recalcula o hash com o mesmo salt
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        // Compara byte a byte
        for (int i = 0; i < 32; i++)
        {
            if (hashBytes[i + 16] != hash[i])
                return false;
        }

        return true;
    }
}
