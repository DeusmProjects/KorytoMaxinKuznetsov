namespace KorytoServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reserveorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Details", "TotalReserve", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Details", "TotalReserve");
        }
    }
}
