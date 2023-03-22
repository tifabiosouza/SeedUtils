# SeedUtils


## PT-BR

Modo de usar:
Crie um arquivo de migration (add-migration) e no arquivo, crie uma lista (pode ser um método novo) que retorna os objetos a serem persistidos no banco de dados. Ex:

    public static List<Person> GetPeople()
    {
        return new()
        {
            new()
            {Name = "Juca", Birth = DateTime.Now},
            new()
            {Name = "Natasha", Birth= DateTime.Now}
        };
    }

Agora, faça uma iteração à lista, para salvar todos os dados, utilizando o migrationBuilder.InsertData() como no exemplo abaixo:

    GetPaymentMethods().ForEach(
        p => migrationBuilder.InsertData(
            table: SeedDataUtils.TableName(p),
            columns: SeedDataUtils.Columns(p),
            values: SeedDataUtils.Values(p)));
Automaticamente, o sistema vai determinar o nome da tabela, desde que a classe do objeto da lista esteja mapeado com a annotation **Table** (Ex: ``[Table("tbl_person")]``) , as colunas a serem persistidas, desde os atributos estejam anotados com o atributo **Column** (Ex: ``[Column("name")])`` e os respectivos valores contidos na lista.
