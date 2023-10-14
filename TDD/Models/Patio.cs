namespace TDD.Models
{
    public class Patio
    {
        public Patio()
        {
            Faturado = 0;
            veiculos = new List<Veiculo>();
        }
        private List<Veiculo> veiculos;
        private double faturado;
        public double Faturado { get => faturado; set => faturado = value; }
        public List<Veiculo> Veiculos { get => veiculos; set => veiculos = value; }
        public double TotalFaturado()
        {
            return this.Faturado;
        }

        public string MostrarFaturamento()
        {
            string totalfaturado = String.Format("Total faturado até o momento :::::::::::::::::::::::::::: {0:c}", this.TotalFaturado());
            return totalfaturado;
        }

        public void RegistrarEntradaVeiculo(Veiculo veiculo)
        {
            veiculo.HoraEntrada = DateTime.Now;
            veiculo.Ticket = this.GerarTicket(veiculo);
            this.Veiculos.Add(veiculo);
        }

        public string RegistrarSaidaVeiculo(String placa)
        {
            Veiculo procurado = null;
            string informacao = string.Empty;

            foreach (Veiculo v in this.Veiculos)
            {
                if (v.Placa == placa)
                {
                    v.HoraSaida = DateTime.Now;
                    TimeSpan tempoPermanencia = v.HoraSaida - v.HoraEntrada;
                    double valorASerCobrado = 0;
                    if (v.Tipo == TipoVeiculo.Automovel)
                    {
                        valorASerCobrado = Math.Ceiling(tempoPermanencia.TotalHours) * 2;

                    }
                    if (v.Tipo == TipoVeiculo.Motocicleta)
                    {
                        valorASerCobrado = Math.Ceiling(tempoPermanencia.TotalHours) * 1;
                    }
                    informacao = string.Format(" Hora de entrada: {0: HH: mm: ss}\n " +
                                             "Hora de saída: {1: HH:mm:ss}\n " +
                                             "Permanência: {2: HH:mm:ss} \n " +
                                             "Valor a pagar: {3:c}", v.HoraEntrada, v.HoraSaida, new DateTime().Add(tempoPermanencia), valorASerCobrado);
                    procurado = v;
                    this.Faturado = this.Faturado + valorASerCobrado;
                    break;
                }

            }
            if (procurado != null)
            {
                this.Veiculos.Remove(procurado);
            }
            else
            {
                return "Não encontrado veículo com a placa informada.";
            }

            return informacao;
        }

        public Veiculo AlteraDadosVeiculo(Veiculo veiculoAlterado)
        {
            var veiculoTemp = (from veiculo in this.Veiculos
                               where veiculo.Placa == veiculoAlterado.Placa
                               select veiculo).SingleOrDefault();
            veiculoTemp.AlteraDados(veiculoAlterado);
            return veiculoTemp;

        }
        public Veiculo PesquisaVeiculo(string placa)
        {
            var encontrado = (from veiculo in this.Veiculos
                              where veiculo.Placa == placa
                              select veiculo).SingleOrDefault();
            return encontrado;
        }

        private string GerarTicket(Veiculo veiculo)
        {
            string identificador = new Guid().ToString().Substring(0, 5);
            veiculo.IdTicket = identificador;
            string ticket = "###Ticket Estacionamento Alura###" +
                            $"Identifcador: {identificador}" +
                            $"Data/Hora de entrada: {DateTime.Now}" +
                            $"Placa do Veículo:{veiculo.Placa}";


            return ticket;
        }
    }
}
