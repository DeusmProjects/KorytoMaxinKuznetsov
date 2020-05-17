namespace KorytoServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletetables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarDetailViewModels", "CarViewModel_Id", "dbo.CarViewModels");
            DropIndex("dbo.CarDetailViewModels", new[] { "CarViewModel_Id" });
            DropTable("dbo.CarViewModels");
            DropTable("dbo.CarDetailViewModels");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
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
            
            CreateIndex("dbo.CarDetailViewModels", "CarViewModel_Id");
            AddForeignKey("dbo.CarDetailViewModels", "CarViewModel_Id", "dbo.CarViewModels", "Id");
        }
    }
}
