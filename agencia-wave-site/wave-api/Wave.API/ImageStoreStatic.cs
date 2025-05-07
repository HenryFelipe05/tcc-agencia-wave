using System;
using System.IO;

public static class ImagemStorage
{
    // Campo estático para armazenar a imagem em formato de byte[]
    public static byte[] Imagem { get; set; }

    // Método para carregar a imagem do arquivo para o campo estático
    public static void CarregarImagem(string caminhoArquivo)
    {
        if (File.Exists(caminhoArquivo))
        {
            Imagem = File.ReadAllBytes(caminhoArquivo); // Converte a imagem para byte[]
        }
        else
        {
            throw new FileNotFoundException("O arquivo de imagem não foi encontrado.", caminhoArquivo);
        }
    }

    // Método para obter a imagem como byte[]
    public static byte[] ObterImagem()
    {
        return Imagem;
    }
}
