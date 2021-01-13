using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utilitarios
{
    public class TratamentoDeImagensPKLOT
    {
        public void Executar()
        {
            var contador = 0;
            var contadorEmpty = 0;
            var contadorOccupied = 0;

            //var baseInputFolder = @"C:\Users\albs_\OneDrive\Documentos\Pós IA\TCC\PKLOT\PKLot.tar\PKLot\PKLotSegmented";
            var baseInputFolder = @"C:\TCC_ForaDoOneDrive\PKLOT\PKLot.tar\PKLot\PKLotSegmented";
            var estacionamento = "UFPR04";
            var numeroVaga = "016";
            string[] condicoesClimaticas = { "Sunny", "Cloudy", "Rainy" };

            //var baseOutputFolder = @"C:\Users\albs_\OneDrive\Documentos\Pós IA\TCC\Imagens selecionadas";
            var baseOutputFolder = @"C:\TCC_ForaDoOneDrive\Imagens selecionadas";


            // Loop por todos os subdiretorios buscando imagens da vaga do número especificado
            foreach (var clima in condicoesClimaticas)
            {
                var diretorio = Path.Combine(baseInputFolder, estacionamento, clima);

                Console.WriteLine(diretorio);

                var subdiretorios = Directory.GetDirectories(diretorio);
                foreach (var subdir in subdiretorios)
                {
                    Console.WriteLine("   " + subdir);

                    var subdiretorios1 = Directory.GetDirectories(subdir);
                    foreach (var diretorioFinal in subdiretorios1)
                    {
                        Console.WriteLine("      " + diretorioFinal);

                        string[] arquivos = Directory.GetFiles(diretorioFinal, "*#" + numeroVaga + ".*");

                        foreach (var origem in arquivos)
                        {
                            contador++;

                            Console.WriteLine("         " + origem);

                            //Copiar arquivo para o diretorio de saida
                            var diretorioDeSaida = Path.Combine(baseOutputFolder, estacionamento, numeroVaga);
                            Directory.CreateDirectory(diretorioDeSaida);

                            var tipoImagem = "Error_";

                            if (origem.Contains("Empty"))
                            {
                                tipoImagem = "Empty_";
                                contadorEmpty++;
                            }
                            else if (origem.Contains("Occupied"))
                            {
                                tipoImagem = "Occupied_";
                                contadorOccupied++;
                            }

                            var arquivoSaida = tipoImagem + clima + "_" + Path.GetFileName(origem);

                            var destino = Path.Combine(diretorioDeSaida, arquivoSaida);

                            //File.Copy(origem, destino);
                        }
                    }

                }

            }

            Console.WriteLine("Numero total de arquivos: " + contador);
            Console.WriteLine("   Empty: " + contadorEmpty);
            Console.WriteLine("   Occupied: " + contadorOccupied);
            Console.ReadKey();
        }
    }
}
