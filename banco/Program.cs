using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banco
{
    abstract class Conta
    {
        protected int numero;
        protected double saldo;


        public void setNumero(int numero)
        {
            if (numero < 1000)
            {
                throw new Exception("Número inválido");
            }
            this.numero = numero;
        }

        public int getNumero()
        {
            return this.numero;
        }

        public double getSaldo()
        {
            return this.saldo;
        }

        public abstract void depositar(double valor);
        public abstract void sacar(double valor);

        public override string ToString()
        {
            return "Número: " + this.numero + " - Saldo: " + this.saldo.ToString("F2");
        }
    }

    class ContaCorrente : Conta
    {
        public ContaCorrente(int numero)
        {
            setNumero(numero);
            this.saldo = 0;
        }

        public override void depositar(double valor)
        {
            if (valor <= 0)
            {
                throw new Exception("Valor inválido");
            }

            this.saldo += valor * 0.99; // desconta 1% de taxa
        }

        public override void sacar(double valor)
        {
            double valorComTaxa = valor * 1.01; // adiciona 1% de taxa
            if (valorComTaxa <= 0)
            {
                throw new Exception("Valor inválido");
            }
            if (valorComTaxa > this.saldo)
            {
                throw new Exception("Saldo insuficiente");
            }
            this.saldo -= valorComTaxa;
            // implementação do saque em conta corrente
        }

        public override string ToString()
        {
            return "Conta Corrente - " + base.ToString();
        }
    }

    class ContaPoupanca : Conta
    {
        public ContaPoupanca(int numero)
        {
            setNumero(numero);
            this.saldo = 0;
        }
        public override void depositar(double valor)
        {
            if (valor <= 0)
            {
                throw new Exception("Valor inválido");
            }
            this.saldo += valor; // sem taxa
        }

        public override void sacar(double valor)
        {
            double valorComTaxa = valor * 1.005; // adiciona 0,5% de taxa
            if (valorComTaxa <= 0)
            {
                throw new Exception("Valor inválido");
            }
            if (valorComTaxa > this.saldo)
            {
                throw new Exception("Saldo insuficiente");
            }
            this.saldo -= valorComTaxa;
            // implementação do saque em conta poupança
        }

        public override string ToString()
        {
            return "Conta Poupança - " + base.ToString();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Conta> contas = new List<Conta>();
            int contadorNumero = 1000;

            do
            {
                Console.WriteLine("1 - Criar Conta Corrente");
                Console.WriteLine("2 - Criar Conta Poupança");
                Console.WriteLine("3 - Listar Contas");
                Console.WriteLine("4 - Depositar");
                Console.WriteLine("5 - Sacar");
                Console.WriteLine("6 - Transferir");
                Console.WriteLine("7 - Sair");

                int opcao;
                Console.Write("Opção: ");
                while (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.Write("Opção inválida\nOpção: ");
                }

                try
                {
                    switch (opcao)
                    {
                        case 1:
                            {
                                contas.Add(new ContaCorrente(contadorNumero));
                                contadorNumero++;
                            }
                            break;
                        case 2:
                            {
                                contas.Add(new ContaPoupanca(contadorNumero));
                                contadorNumero++;
                            }
                            break;
                        case 3:
                            {
                                Console.Clear();
                                foreach (Conta conta in contas)
                                {
                                    Console.WriteLine(conta.ToString());
                                }
                            }
                            break;
                        case 4:
                            {
                                Console.WriteLine("Informe o número da conta.");
                                int numero;
                                while (!int.TryParse(Console.ReadLine(), out numero))
                                {
                                    Console.WriteLine("va");
                                };

                                Conta conta = contas.Find(c => c.getNumero() == numero);

                                if (conta == null)
                                {
                                    throw new Exception("conta nao encontrada");
                                }
                                Console.WriteLine("Quanto deseja depositar:");
                                double valordeposito;
                                while (!double.TryParse(Console.ReadLine(), out valordeposito)) ;
                                conta.depositar(valordeposito);
                                Console.WriteLine("deposito realizado com suceso!!!");
                            }
                            break;
                        case 5:
                            {
                                Console.WriteLine("Informe o número da conta.");
                                int numero;
                                while (!int.TryParse(Console.ReadLine(), out numero))
                                {
                                    Console.WriteLine("va");
                                };

                                Conta conta = contas.Find(c => c.getNumero() == numero);

                                if (conta == null)
                                {
                                    throw new Exception("conta nao encontrada");
                                }
                                Console.WriteLine("Quanto deseja depositar:");
                                double valorsaque;
                                while (!double.TryParse(Console.ReadLine(), out valorsaque)) ;
                                conta.sacar(valorsaque);
                                Console.WriteLine("deposito realizado com suceso!!!");
                            }
                            break;
                        case 6:
                            {
                                // Transferir
                            }
                            break;
                        case 7:
                            {
                                // Sair do programa
                                return;
                            }
                            break;
                        default:
                            {
                                Console.WriteLine("Opção inválida");
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                    continue; // volta ao início do loop
                }


            } while (true);
        }
    }
}
