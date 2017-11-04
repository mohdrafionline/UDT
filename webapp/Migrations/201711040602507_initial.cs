namespace SmartAdminMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AggrementTypes",
                c => new
                    {
                        AggrementTypeID = c.Int(nullable: false, identity: true),
                        AggrementTypeName = c.String(),
                    })
                .PrimaryKey(t => t.AggrementTypeID);
            
            CreateTable(
                "dbo.Billables",
                c => new
                    {
                        BillableID = c.Int(nullable: false, identity: true),
                        BillableName = c.String(),
                    })
                .PrimaryKey(t => t.BillableID);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        StateID = c.Int(nullable: false),
                        CityName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.CityID);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        CountryName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        CountryID = c.Int(nullable: false),
                        StateName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.StateID)
                .ForeignKey("dbo.Country", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        PositionName = c.String(),
                    })
                .PrimaryKey(t => t.PositionId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffID = c.Int(nullable: false, identity: true),
                        FirstName = c.Int(nullable: false),
                        LastName = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        Address = c.String(),
                        City = c.Int(nullable: false),
                        Region = c.Int(nullable: false),
                        PostalCode = c.Int(nullable: false),
                        Country = c.Int(nullable: false),
                        Contractor = c.String(),
                        W9Form = c.Binary(),
                        StaffPhoto = c.Binary(),
                        StaffNumber = c.String(),
                        JobTitle = c.String(),
                        DepartmentID = c.Int(nullable: false),
                        EmailID = c.String(),
                        Password = c.String(),
                        MobilePhone = c.String(),
                        BirthDate = c.String(),
                        Title = c.String(),
                        HireDate = c.DateTime(nullable: false),
                        Phone = c.Long(nullable: false),
                        Extension = c.Int(nullable: false),
                        TerminationDate = c.DateTime(nullable: false),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.StaffID);
            
            CreateTable(
                "dbo.tblEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailID = c.String(),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                    })
                .PrimaryKey(t => t.TimeHeaderID);
            
            CreateTable(
                "dbo.WorkRoles",
                c => new
                    {
                        WorkRoleID = c.Int(nullable: false, identity: true),
                        WorkRoleName = c.String(),
                    })
                .PrimaryKey(t => t.WorkRoleID);
            
            CreateTable(
                "dbo.WorkTypes",
                c => new
                    {
                        WorkTypeID = c.Int(nullable: false, identity: true),
                        WorkTypeName = c.String(),
                    })
                .PrimaryKey(t => t.WorkTypeID);
            
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
