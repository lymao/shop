namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnDetailToProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Detail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Detail");
        }
    }
}
