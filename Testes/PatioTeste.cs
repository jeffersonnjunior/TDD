using TDD.Models;
using Xunit.Abstractions;

namespace Testes
{
    public class PatioTeste : IDisposable
    {
        private Veiculo veiculo = new Veiculo();
        public ITestOutputHelper Output { get; }
        public PatioTeste(ITestOutputHelper output)
        {
            Output = output;
            Output.WriteLine("Execução do Construtor");

            veiculo.Proprietario = "André Silva";
            veiculo.Placa = "ASD-9999";
            veiculo.Cor = "Preto";
            veiculo.Modelo = "Fusca";

        }

        [Fact]
        public void ValidaFaturamentoDoEstacionamentoComUmVeiculo()
        {
            Patio estacionamento = new();

            veiculo.Proprietario = "André Silva";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Placa = "ABC-0101";
            veiculo.Modelo = "Fusca";
            veiculo.Acelerar(10);
            veiculo.Frear(5);
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            double faturamento = estacionamento.TotalFaturado();

            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "preto", "Gol")]
        [InlineData("Jose Silva", "POL-9242", "Cinza", "Fusca")]
        [InlineData("Maria Silva", "GDR-6524", "Azul", "Opala")]
        public void ValidaFaturamentoComVariosVeiculosNoEstacionamento(string proprietario, string placa, string cor, string modelo)
        {
            Patio estacionamento = new();

            veiculo.Proprietario = proprietario;
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Acelerar(10);
            veiculo.Frear(5);
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            double faturamento = estacionamento.TotalFaturado();

            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("André Silva", "ASD-1234", "Verde", "Fusca")]
        public void LocalizaUmVeiculoNoEstacionamentoComBaseNaPlaca(string proprietario, string placa, string cor, string modelo)
        {
            Patio estacionamento = new();
          
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Acelerar(10);
            veiculo.Frear(5);
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var consultado = estacionamento.PesquisaVeiculo(placa);

            Assert.Equal(placa, consultado.Placa);
        }

        public void Dispose()
        {
            Output.WriteLine("Execução do Cleanup");
        }
    }
}
