namespace AppPet.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addservico : DbMigration
    {
        public override void Up()
        {
           
            
            CreateTable(
                "dbo.Servicos",
                c => new
                    {
                        ServicoId = c.Int(nullable: false, identity: true),
                        TipoServicoId = c.Int(nullable: false),
                        RacaId = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(maxLength: 500),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DtInc = c.DateTime(),
                        DtAlt = c.DateTime(),
                        UserInc = c.String(),
                        UserAlt = c.String(),
                    })
                .PrimaryKey(t => t.ServicoId)
                .ForeignKey("dbo.Racas", t => t.RacaId, cascadeDelete: true)
                .ForeignKey("dbo.TiposServicos", t => t.TipoServicoId, cascadeDelete: true)
                .Index(t => t.TipoServicoId)
                .Index(t => t.RacaId);
            
        }
        
        public override void Down()
        {
        }
    }
}
