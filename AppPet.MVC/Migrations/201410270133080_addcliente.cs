namespace AppPet.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcliente : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Telefone = c.String(maxLength: 20),
                        Celular = c.String(maxLength: 20),
                        Facebook = c.String(maxLength: 100),
                        DtNascimento = c.DateTime(),
                        Cep = c.String(maxLength: 9),
                        Endereco = c.String(maxLength: 200),
                        Numero = c.String(maxLength: 10),
                        Complemento = c.String(maxLength: 100),
                        Bairro = c.String(maxLength: 100),
                        Cidade = c.String(maxLength: 100),
                        Uf = c.String(maxLength: 2),
                        Descricao = c.String(maxLength: 500),
                        DtInc = c.DateTime(),
                        DtAlt = c.DateTime(),
                        UserInc = c.String(),
                        UserAlt = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Clientes");
        }
    }
}
