using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utilitarios
{
    public class TratamentoDeImagensCNRPARK
    {
        public void Executar()
        {
            var contador = 0;
            var contadorVazia = 0;
            var contadorOcupada = 0;

            var contadorTreinamentoVazia = 0;
            var contadorTreinamentoOcupada = 0;

            const int TOTAL_IMAGENS_TREINAMENTO_VAZIAS = 50;
            const int TOTAL_IMAGENS_TREINAMENTO_OCUPADAS = 50;

            var dirBaseEntrada = @"C:\TCC_ForaDoOneDrive\CNRPark-Patches-150x150";
            var estacionamento = "A";

            var dirBaseSaidaTreinamento = @"C:\TCC_ForaDoOneDrive\CNRPark-Patches-150x150\A - Imagens tratadas - treinamento";
            var dirBaseSaidaBusca = @"C:\TCC_ForaDoOneDrive\CNRPark-Patches-150x150\A - Imagens tratadas - busca";

            // Criar os diretórios de saída, caso não existam
            Directory.CreateDirectory(dirBaseSaidaTreinamento);
            Directory.CreateDirectory(dirBaseSaidaBusca);

            var diretorio = Path.Combine(dirBaseEntrada, estacionamento);

            Console.WriteLine(diretorio);

            var subdiretorios = Directory.GetDirectories(diretorio);
            foreach (var subdir in subdiretorios)
            {
                Console.WriteLine("   " + subdir);

                string[] arquivos = Directory.GetFiles(subdir, "*.jpg");

                foreach (var arquivoOrigem in arquivos)
                {
                    var tipoImagem = "error_";

                    if (arquivoOrigem.Contains("free"))
                    {
                        tipoImagem = "free_";
                        contadorVazia++;
                    }
                    else if (arquivoOrigem.Contains("busy"))
                    {
                        tipoImagem = "busy_";
                        contadorOcupada++;
                    }

                    var nomeImagemDestino = tipoImagem + Path.GetFileName(arquivoOrigem);
                    var caminhoCompletoImagemDestino = Path.Combine(dirBaseSaidaBusca, nomeImagemDestino);

                    // Escolher aleatoriamente as imagens de treinamento
                    Random rnd = new Random();
                    int aleatorio = rnd.Next(9);     // numero aleatório entre 0 e 9
                    if (aleatorio == 0)
                    {
                        if (tipoImagem == "free_" && contadorTreinamentoVazia < TOTAL_IMAGENS_TREINAMENTO_VAZIAS ||
                            tipoImagem == "busy_" && contadorTreinamentoOcupada < TOTAL_IMAGENS_TREINAMENTO_OCUPADAS)
                        {
                            // copiar imagem para diretorio de treinamento
                            caminhoCompletoImagemDestino = Path.Combine(dirBaseSaidaTreinamento, nomeImagemDestino);

                            if (tipoImagem == "free_")
                            {
                                contadorTreinamentoVazia++;
                            }
                            else
                            { 
                                contadorTreinamentoOcupada++; 
                            }
                        }
                    }
                    
                    File.Copy(arquivoOrigem, caminhoCompletoImagemDestino);

                    contador++;
                    Console.WriteLine("         " + arquivoOrigem);
                }
            }

            Console.WriteLine("Numero total de arquivos: " + contador);
            Console.WriteLine("   Empty: " + contadorVazia);
            Console.WriteLine("   Occupied: " + contadorOcupada);
            Console.ReadKey();
        }
    }
}
