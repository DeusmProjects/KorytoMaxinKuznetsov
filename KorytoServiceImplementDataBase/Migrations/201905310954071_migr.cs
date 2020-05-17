namespace KorytoServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarDetailViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        DetailId = c.Int(nullable: false),
                        DetailName = c.String(),
                        Amount = c.Int(nullable: false),
                        CarViewModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarViewModels", t => t.CarViewModel_Id)
                .Index(t => t.CarViewModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarDetailViewModels", "CarViewModel_Id", "dbo.CarViewModels");
            DropIndex("dbo.CarDetailViewModels", new[] { "CarViewModel_Id" });
            DropTable("dbo.CarDetailViewModels");
            DropTable("dbo.CarViewModels");
        }
    }
}
