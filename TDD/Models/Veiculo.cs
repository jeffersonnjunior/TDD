namespace TDD.Models
{
    public class Veiculo
    {
        //Campos      
        private string _placa;
        private string _proprietario;
        private TipoVeiculo _tipo;
        private string _ticket;

        //Propriedades
        public string Ticket { get { return _ticket; } set { _ticket = value; } }
        public string IdTicket { get; set; }

        public string Placa
        {
            get
            {
                return _placa;
            }
            set
            {
                if (value.Length != 8)
                {
                    throw new FormatException(" A placa deve possuir 8 caracteres");
                }
                for (int i = 0; i < 3; i++)
                {
                    
                    if (char.IsDigit(value[i]))
                    {
                        throw new FormatException("Os 3 primeiros caracteres devem ser letras!");
                    }
                }
                
                if (value[3] != '-')
                {
                    throw new FormatException("O 4° caractere deve ser um hífen");
                }

                for (int i = 4; i < 8; i++)
                {
                    if (!char.IsDigit(value[i]))
                    {
                        throw new FormatException("Do 5º ao 8º caractere deve-se ter um número!");
                    }
                }
                _placa = value;

            }
        }
        public string Cor { get; set; }
        public double Largura { get; set; }
        public double VelocidadeAtual { get; set; }
        public string Modelo { get; set; }
        public string Proprietario
        {
            get
            {
                return _proprietario;
            }
            set
            {
                if (value.Length < 3)
                {
                    throw new System.FormatException(" Nome de proprietário deve ter no mínimo 3 caracteres.");
                }
                _proprietario = value;
            }

        }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSaida { get; set; }

        public TipoVeiculo Tipo { get => _tipo; set => _tipo = value; }

        //Métodos
        public void Acelerar(int tempoSeg)
        {
            this.VelocidadeAtual += (tempoSeg * 10);
        }

        public void Frear(int tempoSeg)
        {
            this.VelocidadeAtual -= (tempoSeg * 15);
        }

        public void AlteraDados(Veiculo veiculoAlterado)
        {
            this.Proprietario = veiculoAlterado.Proprietario;
            this.Modelo = veiculoAlterado.Modelo;
            this.Largura = veiculoAlterado.Largura;
            this.Cor = veiculoAlterado.Cor;
        }

        public override string ToString()
        {
            return $"Ficha do Veículo:\n " +
                    $"Tipo do Veículo: {this.Tipo.ToString()}\n " +
                    $"Proprietário: {this.Proprietario}\n" +
                    $"Modelo: {this.Modelo}\n" +
                    $"Cor: {this.Cor}\n" +
                    $"Placa: {this.Placa}\n";

        }
        public Veiculo()
        {

        }

        public Veiculo(string proprietario)
        {
            Proprietario = proprietario;
        }
    }
}