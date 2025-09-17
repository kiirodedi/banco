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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Número inválido.");
                Console.ResetColor();
                throw new Exception();
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

        public void Transferir(Conta destino, double valor) 
        {
            this.sacar(valor);
            destino.depositar(this.saldo);
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Valor inválido");
                Console.ResetColor();
                throw new Exception();
            }

            this.saldo += valor * 0.99; // desconta 1% de taxa
        }

        public override void sacar(double valor)
        {
            double valorComTaxa = valor * 1.01; // adiciona 1% de taxa
            if (valorComTaxa <= 0)
            {
                
                Console.WriteLine("Valor inválido.");
                Console.ResetColor();
                throw new Exception();
            }
            if (valorComTaxa > this.saldo)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Saldo insuficiente.");
                Console.ResetColor();
                throw new Exception();
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Valor inválido.");
                Console.ResetColor();
                throw new Exception();
            }
            this.saldo += valor; // sem taxa
        }

        public override void sacar(double valor)
        {
            double valorComTaxa = valor * 1.005; // adiciona 0,5% de taxa
            if (valorComTaxa <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Valor inválido.");
                Console.ResetColor();
                throw new Exception();
            }
            if (valorComTaxa > this.saldo)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Saldo insuficiente.");
                Console.ResetColor();
                throw new Exception();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Opção inválida\nOpção: ");
                    Console.ResetColor();
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
                                    Console.WriteLine("Informe um número válido.");
                                };

                                Conta conta = contas.Find(c => c.getNumero() == numero);

                                if (conta == null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Conta não encontrada.");
                                    Console.ResetColor();
                                    throw new Exception();
                                    
                                }
                                Console.WriteLine("Quanto deseja depositar?");
                                double valordeposito;
                                while (!double.TryParse(Console.ReadLine(), out valordeposito)) ;
                                conta.depositar(valordeposito);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Depósito realizado com suceso!");
                                Console.ResetColor();
                            }
                            break;
                        case 5:
                            {
                                Console.WriteLine("Informe o número da conta:");
                                int numero;
                                while (!int.TryParse(Console.ReadLine(), out numero))
                                {
                                    Console.WriteLine("Informe o número da conta:");
                                };

                                Conta conta = contas.Find(c => c.getNumero() == numero);

                                if (conta == null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Conta não encontrada.");
                                    Console.ResetColor();
                                    throw new Exception();
                                    
                                }
                                Console.WriteLine("Quanto deseja sacar?");
                                double valorsaque;
                                while (!double.TryParse(Console.ReadLine(), out valorsaque)) ;
                                conta.sacar(valorsaque);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Saque realizado com suceso!");
                                Console.ResetColor();
                            }
                            break;
                        case 6:
                            {
                                // Conta origem
                                Console.WriteLine("Informe o número da conta de origem:");
                                int numeroOrigem;
                                while (!int.TryParse(Console.ReadLine(), out numeroOrigem))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Digite um número válido.");
                                    Console.ResetColor();
                                }

                                Conta contaOrigem = contas.Find(c => c.getNumero() == numeroOrigem);

                                if (contaOrigem == null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Conta não encontrada.");
                                    Console.ResetColor();
                                    throw new Exception();
                                }

                                // Conta destino
                                Console.WriteLine("Informe o número da conta de destino:");
                                int numeroDestino;
                                while (!int.TryParse(Console.ReadLine(), out numeroDestino))
                                {
                                    Console.WriteLine("Informe o número da conta de destino:");
                                }

                                Conta contaDestino = contas.Find(c => c.getNumero() == numeroDestino);

                                if (contaDestino == null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Conta não encontrada.");
                                    Console.ResetColor();
                                    throw new Exception();
                                }

                                Console.WriteLine("Quanto deseja transferir?");
                                int valorTransferencia;
                                while (!int.TryParse(Console.ReadLine(), out valorTransferencia));
                                contaOrigem.Transferir(contaDestino, valorTransferencia);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Transferência realizada com suceso!");
                                Console.ResetColor();
                            }
                            break;
                        case 7:
                            {
                                Console.WriteLine("Saindo do programa...");
                                Environment.Exit(0);
                                return;
                            }
                            break;
                        default:
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Opção inválida.");
                                Console.ResetColor();
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
