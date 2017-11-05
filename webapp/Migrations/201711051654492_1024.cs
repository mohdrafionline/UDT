namespace SmartAdminMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1024 : DbMigration
    {
        public override void Up()
        {
           
            CreateTable(
                "dbo.TimeDetails",
                c => new
                    {
                        TimeDetailID = c.Int(nullable: false, identity: true),
                        TimeIn = c.Time(nullable: false, precision: 7),
                        TimeOut = c.Time(nullable: false, precision: 7),
                        TimeDeduct = c.Time(nullable: false, precision: 7),
                        WorkTypeID = c.Int(nullable: false),
                        BillableID = c.Int(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.TimeDetailID);
            
            CreateTable(
                "dbo.TimeHeaders",
                c => new
                    {
                        TimeHeaderID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        DivisonID = c.Int(nullable: false),
                        WorkRoleID = c.Int(nullable: false),
                        Aggrement = c.String(),
                        TimeDate = c.DateTime(nullable: false),
                        TimeDetailID = c.Int(nullable: false),
                        AggrementTypeID = c.Int(nullable: false),
                        Overnight = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TimeHeaderID);
            
           
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.State", "CountryID", "dbo.Country");
            DropIndex("dbo.State", new[] { "CountryID" });
            DropTable("dbo.WorkTypes");
            DropTable("dbo.WorkRoles");
            DropTable("dbo.TimeHeaders");
            DropTable("dbo.TimeDetails");
            DropTable("dbo.tblUsers");
            DropTable("dbo.tblEmployees");
            DropTable("dbo.Staffs");
            DropTable("dbo.Positions");
            DropTable("dbo.Departments");
            DropTable("dbo.State");
            DropTable("dbo.Country");
            DropTable("dbo.City");
            DropTable("dbo.Billables");
            DropTable("dbo.AggrementTypes");
        }
    }
}
