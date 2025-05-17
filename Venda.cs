using System;
using System.Collections.Generic; // Importa o namespace System.Collections.Generic, que contém a interface List<T> para criar listas

namespace ProjetoVendasBasico
{
    public class Venda //Declara uma classe pública chamada "Venda"
    {
        public List<Produto> ItensVendidos { get; set; } // Declara uma propriedade pública chamada "ItensVendidos"
                                                         // É uma lista (`List`) que conterá objetos do tipo `Produto`
                                                         // Esta lista armazenará todos os produtos que foram adicionados à venda
                                                         // O "{ get; set; }" permite ler e modificar esta lista

        public decimal ValorTotal { get; private set; } // Declara uma propriedade pública chamada "ValorTotal" do tipo decimal
                                                        // Esta propriedade armazenará o valor total da venda
                                                        // O "private set" significa que o valor de ValorTotal só pode ser modificado dentro da própria classe Venda


        public string FormaPagamento { get; set; } // Declara uma propriedade pública chamada "FormaPagamento" do tipo string
                                                   // Armazenará a forma de pagamento escolhida pelo cliente (Dinheiro, Pix ou Cartão)
                                                   // O "{ get; set; }" permite ler e modificar esta string

        // Este é o construtor da classe Venda. Ele é chamado quando um novo objeto Venda é criado
        public Venda()
        {
            ItensVendidos = new List<Produto>(); // Inicializa a lista ItensVendidos como uma nova lista vazia de objetos Produto.
            ValorTotal = 0;                      // Inicializa a propriedade ValorTotal com o valor zero.
            FormaPagamento = "";                 // Inicializa a propriedade FormaPagamento com uma string vazia
        }

        // Este método público permite adicionar um produto à venda
        public void AdicionarItem(Produto produto)
        {
            ItensVendidos.Add(produto); // Adiciona o objeto 'produto' recebido por parâmetro à lista ItensVendidos
            ValorTotal += produto.Preco; // Incrementa o ValorTotal da venda com o preço do produto adicionado
        }

        // Este método público calcula o troco, caso a forma de pagamento seja dinheiro
        public decimal CalcularTroco(decimal valorRecebido)
        {
            // Verifica se a forma de pagamento (convertida para minúsculo para comparação) é "dinheiro"
            // E se o valor recebido pelo cliente é maior ou igual ao valor total da venda.
            if(FormaPagamento.ToLower() == "dinheiro" && valorRecebido >= ValorTotal) {
                return valorRecebido - ValorTotal; // Retorna o valor do troco (valor recebido menos o valor total).
            }
            return 0; // Retorna 0 se a forma de pagamento não for dinheiro ou se o valor recebido for insuficiente.
        }
    }
}
