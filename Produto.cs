namespace ProjetoVendasBasico
{
    public class Produto // Declara uma classe pública chamada "Produto". Classes sao como "modelos" para criar objetos
    {
        public string Nome { get; set; } // Declara uma proprieda pública chamada "Nome" do tipo string (texto)
                                         // O "{ get; set; }" permite que o valor do Nome seja lido (get) e modificado (set)
        
        public decimal Preco { get; set; } // Declara uma propriedade pública chamada "Preco" do tipo decimal (para valores monetários)
                                           // Usar decimal é mais preciso para dinheiro do que float ou double
                                           // O "{ get; set; }" permite que o valor do Preço seja lido e modificado
        public string Caracteristica { get; set; } // Declara uma propriedade pública chamada "Caracteristica" do tipo string
                                                   // Esta propriedade armazenará informações como tamanho ou volume do produto
                                                   // O "{ get; set; }" permite que o valor da Caracteristica seja lido (get) e modificado (set)
        
        // Este é o construtor da classe Produto.Ele é chamado quando um novo objeto Produto é criado
        public Produto(string nome, decimal preco, string caracteristica)
        {
            Nome = nome; // Atribui o valor passado para o parâmetro 'nome' à propriedade 'Nome' do objeto
            Preco = preco; // Atribui o valor passado para o parâmetro 'preco' à propriedade 'Preco' do objeto
            Caracteristica = caracteristica; // Atribui o valor passado para o parâmetro 'caracteristica' à propriedade 'Caracteristica' do objeto
        }

        // Este método sobrescreve o método ToString() padrão
        // ToString() é chamado quando você tenta representar um objeto Produto como uma string
        public override string ToString()
        {
            // Retorna uma string formatada que representa o objeto Produto
            // $"{Nome} - {Caracteristica} - R$ {Preco:F2}" usa interpolação de strings para inserir os valores das propriedades
            // ":F2" formata o preço para exibir duas casas decimais
            return $"{Nome} - {Caracteristica} - R$ {Preco:F2}";
        }
    }
}
