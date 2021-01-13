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

            var dirBaseEntrada = @"C:\TCC_ForaDoOneDrive\CNRPark-Patches-150x150";
            var estacionamento = "A";

            var dirBaseSaidaTreinamento = @"C:\TCC_ForaDoOneDrive\CNRPark-Patches-150x150\A - Imagens tratadas - treinamento";
            var dirBaseSaidaBusca = @"C:\TCC_ForaDoOneDrive\CNRPark-Patches-150x150\A - Imagens tratadas - busca";


            var diretorio = Path.Combine(dirBaseEntrada, estacionamento);

            Console.WriteLine(diretorio);

            var subdiretorios = Directory.GetDirectories(diretorio);
            foreach (var subdir in subdiretorios)
            {
                Console.WriteLine("   " + subdir);

                string[] arquivos = Directory.GetFiles(subdir, "*.jpg");

                foreach (var arquivoOrigem in arquivos)
                {
                    contador++;

                    Console.WriteLine("         " + arquivoOrigem);

                    ////Copiar arquivo para o diretorio de saida
                    //var diretorioDeSaida = Path.Combine(dirBaseSaida, estacionamento, numeroVaga);
                    //Directory.CreateDirectory(diretorioDeSaida);

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

                    //Random rnd = new Random();
                    //int dice = rnd.Next(1, 7);   // creates a number between 1 and 6
                    //int card = rnd.Next(52);     // creates a number between 0 and 51

                    //var arquivoSaida = tipoImagem + clima + "_" + Path.GetFileName(arquivoOrigem);

                    //var destino = Path.Combine(diretorioDeSaida, arquivoSaida);

                    //File.Copy(origem, destino);
                }
            }

            Console.WriteLine("Numero total de arquivos: " + contador);
            Console.WriteLine("   Empty: " + contadorVazia);
            Console.WriteLine("   Occupied: " + contadorOcupada);
            Console.ReadKey();
        }
    }
}
