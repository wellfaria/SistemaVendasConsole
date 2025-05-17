using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace ProjetoVendasBasico
{
    public class SistemaVendas
    {
        private List<Produto> produtosCadastrados; // Lista para armazenar os produtos cadastrados

        public SistemaVendas()
        {
            produtosCadastrados = new List<Produto>(); // Inicializa a lista de produtos ao criar uma instância de SistemaVendas
        }

        // Método para exibir o menu principal do sistema
        public void ExibirMenuPrincipal()
        {
            bool executando = true;
            while(executando) {
                Console.WriteLine("----- Sistema de Vendas -----");
                Console.WriteLine("1 - Realizar Venda");
                Console.WriteLine("2 - Administraçao");
                Console.WriteLine("3 - Sair");
                Console.Write("Escolha uma opçao: ");

                string escolha = Console.ReadLine();

                switch(escolha) {
                    case "1":
                        RealizarVenda();
                        break;
                    case "2":
                        AcessarAdministracao();
                        break;
                    case "3":
                        executando = false;
                        Console.WriteLine("Saindo do Sistema...");
                        break;
                    default:
                        Console.WriteLine("Opçao inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine(); // Adiciona uma linha em branco para melhor visualização
            }
        }

        // Método para lidar com o processo de venda
        private void RealizarVenda()
        {
            if (produtosCadastrados.Count == 0) {
                Console.WriteLine("Nenhum produto cadastrado. Cadastre algum produto");
                return;
            }

            Venda vendaAtual = new Venda(); // Cria uma nova instância da classe Venda

            bool vendendo = true;
            while (vendendo) {
                Console.WriteLine("\n----- Realizar Venda -----");
                ExibirProdutosDisponiveis(); // Exibe a lista de produtos cadastrados
                Console.Write("Digite o número do produto para adicionar a venda (ou `0` para finalizar): ");
                string escolhaProduto = Console.ReadLine();

                if (escolhaProduto == "0") {
                    vendendo = false;
                    FinalizarVenda(vendaAtual);
                }
                else if (int.TryParse(escolhaProduto, out int indiceProduto) && indiceProduto > 0 && indiceProduto <= produtosCadastrados.Count) {
                    Produto produtoSelecionado = produtosCadastrados[indiceProduto - 1];
                    vendaAtual.AdicionarItem(produtoSelecionado);
                    Console.WriteLine($"{produtoSelecionado.Nome} adicionado a venda. Total parcial: R$ {vendaAtual.ValorTotal:F2}");
                }
                else {
                    Console.WriteLine("Opçao inválida. Tente novamente.");
                }
            }
        }

        // Método para exibir os produtos cadastrados com um número para seleção
        private void ExibirProdutosDisponiveis()
        {
            Console.WriteLine("----- Produtos Disponíveis -----");
            for (int i = 0; i < produtosCadastrados.Count; i++) {
                Console.WriteLine($"{i + 1} - {produtosCadastrados[i]}"); // Utiliza o ToString() da classe Produto
            }
        }

        // Método para finalizar a venda, perguntar a forma de pagamento e calcular o troco
        private void FinalizarVenda(Venda venda)
        {
            if (venda.ItensVendidos.Count == 0) {
                Console.WriteLine("Nenhum item adicionado a venda.");
                return;
            }

            Console.WriteLine("\n----- Finalizar Venda -----");
            Console.WriteLine($"Total da venda: R$ {venda.ValorTotal}");

            Console.WriteLine("Forma de pagamento (Dinheiro, Pix ou Cartao): ");
            string formaPagamento = Console.ReadLine();
            venda.FormaPagamento = formaPagamento;

            if (formaPagamento.ToLower() == "dinheiro") {
                Console.Write("Valor recebido: R$ ");
                if (decimal.TryParse(Console.ReadLine(), out decimal valorRecebido)) {
                    decimal troco = venda.CalcularTroco(valorRecebido);
                    Console.WriteLine($"Troco: R$ {troco:F2}");
                    Console.WriteLine("Venda concluida com sucesso!");
                }
                else {
                    Console.WriteLine("Valor inválido.");
                }
            }
            else if (formaPagamento.ToLower() == "pix" || formaPagamento.ToLower() == "cartao") {
                Console.WriteLine("Venda concluida com sucesso!");
            }
            else {
                Console.WriteLine("Forma de pagamento inválida.");
            }
        }

        // Método para acessar a área de administração (protegida por senha)
        private void AcessarAdministracao()
        {
            Console.Write("\nDigite a senha de administrador: ");
            string senhaDigitada = Console.ReadLine();

            // Aqui você colocaria a lógica de verificação da senha (ex: comparar com uma senha fixa)
            if (senhaDigitada == "123456") {    // Substitua "123456" pela senha real
                ExibirMenuAdministracao();
            } 
            else {
                Console.WriteLine("Acesso negado. Senha incorreta.");
            }
        }

        // Método para exibir o menu de administração
        private void ExibirMenuAdministracao()
        {
            bool administrando = true;
            while (administrando) {
                Console.WriteLine("\n----- Administraçao -----");
                Console.WriteLine("1 - Cadastrar Produto");
                Console.WriteLine("2 - Listar Produtos Cadastrados");
                Console.WriteLine("3 - Voltar ao Menu Principal");
                Console.Write("Escolha uma opçao: ");

                string escolhaAdmin = Console.ReadLine();

                switch (escolhaAdmin) {
                    case "1":
                        CadastrarProduto();
                        break;
                    case "2":
                        ListarProdutosCadastrados();
                        break;
                    case "3":
                        administrando = false;
                        break;
                    default:
                        Console.WriteLine("Opçao inválida. Tente novamente.");
                        break;

                }
            }
        }

        // Método para cadastrar um novo produto
        private void CadastrarProduto()
        {
            Console.WriteLine("\n----- Cadastrar Produto -----");
            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Preço do produto: R$ ");
            if (decimal.TryParse(Console.ReadLine(), out decimal preco)) {
                Console.Write("Característica (Tamanho/Volume): ");
                string caracteristica = Console.ReadLine();

                Produto novoProduto = new Produto(nome, preco, caracteristica);
                produtosCadastrados.Add(novoProduto);
                Console.WriteLine($"{novoProduto.Nome} cadastrado com sucesso!");
            }
            else {
                Console.WriteLine("Preço inválido.");
            }

        }

        // Método para listar os produtos cadastrados
        private void ListarProdutosCadastrados()
        {
            Console.WriteLine("\n----- Produtos Cadastrados -----");
            if (produtosCadastrados.Count == 0) {
                Console.WriteLine("Nenhum produto cadastrado");
            }
            else {
                foreach(var produto in produtosCadastrados) {
                    Console.WriteLine(produto); // Utiliza o ToString() da classe Produto
                }
            }
        }

        // Método Main (ponto de entrada do programa)
        static void Main(string[] args)
        {
            SistemaVendas sistema = new SistemaVendas();
            sistema.ExibirMenuPrincipal();
        }
    }

}
