using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine();

            // Validação da placa (formato brasileiro: ABC-1234 ou ABC1D23 - Mercosul)
            if (ValidarPlaca(placa))
            {
                // Verifica se o veículo já está estacionado
                if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
                {
                    Console.WriteLine("Esse veículo já está estacionado aqui.");
                }
                else
                {
                    veiculos.Add(placa.ToUpper());
                    Console.WriteLine($"Veículo {placa} adicionado com sucesso!");
                }
            }
            else
            {
                Console.WriteLine("Placa inválida! Use o formato ABC-1234 (padrão) ou ABC1D23 (Mercosul).");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            // Pede para o usuário digitar a placa e armazena na variável placa
            string placa = Console.ReadLine();

            // Verifica se o veículo existe
            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                // Pede para o usuário digitar a quantidade de horas
                int horas = int.Parse(Console.ReadLine());

                // Realiza o cálculo: "precoInicial + precoPorHora * horas"
                decimal valorTotal = precoInicial + (precoPorHora * horas);

                // Remove a placa digitada da lista de veículos
                veiculos.Remove(veiculos.First(x => x.ToUpper() == placa.ToUpper()));

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");

                // Realiza um laço de repetição e lista os veículos estacionados
                int contador = 1;
                foreach (string veiculo in veiculos)
                {
                    Console.WriteLine($"{contador} - {veiculo}");
                    contador++;
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        // Método para validar placa brasileira
        private bool ValidarPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return false;

            // Remove espaços e hífens para facilitar a validação
            placa = placa.Replace("-", "").Replace(" ", "").ToUpper();

            // Padrão antigo: ABC1234 (3 letras + 4 números)
            Regex padraoAntigo = new Regex(@"^[A-Z]{3}[0-9]{4}$");

            // Padrão Mercosul: ABC1D23 (3 letras + 1 número + 1 letra + 2 números)
            Regex padraoMercosul = new Regex(@"^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$");

            return padraoAntigo.IsMatch(placa) || padraoMercosul.IsMatch(placa);
        }
    }
}