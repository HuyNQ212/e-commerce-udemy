using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataWithSQlRaw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
-- Insert data from json files
-- Product brands
DECLARE @json NVARCHAR(MAX)

SELECT @json = BulkColumn 
FROM OPENROWSET (BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/json/brands.json', SINGLE_CLOB) as j


INSERT INTO dbo.ProductBrands
(
    Name
)
SELECT Name
FROM OPENJSON(@json) 
WITH (
  Name NVARCHAR(MAX) '$.Name'
)
GO

-- Product brands
DECLARE @json NVARCHAR(MAX)

SELECT @json = BulkColumn 
FROM OPENROWSET (BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/json/types.json', SINGLE_CLOB) as j


INSERT INTO dbo.ProductTypes
(
    Name
)
SELECT Name
FROM OPENJSON(@json) 
WITH (
  Name NVARCHAR(MAX) '$.Name'
)
GO

-- Products table
DECLARE @json NVARCHAR(MAX);

SELECT @json = BulkColumn  
FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/json/products.json', SINGLE_CLOB) AS j;

DECLARE @pictureUrl NVARCHAR(MAX);
DECLARE @picture VARBINARY(MAX);

INSERT INTO Products(Name, Description, Price, ProductTypeId, ProductBrandId, PictureUrl)
SELECT 
   j.Name,
   j.Description,
   j.Price,
   j.ProductTypeId, 
   j.ProductBrandId,
   j.PictureUrl
FROM OPENJSON(@json)
WITH (
   Name nvarchar(100) '$.Name',
   Description nvarchar(1000) '$.Description',
   Price decimal(18,2) '$.Price',   
   ProductTypeId int '$.ProductTypeId',
   ProductBrandId int '$.ProductBrandId',
   PictureUrl nvarchar(max) '$.PictureUrl'
) AS j;
GO

-- PictureUrl to Picture
UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/sb-ang1.png', SINGLE_BLOB) AS img
)
WHERE Id = 1;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/sb-ang2.png', SINGLE_BLOB) AS img
)
WHERE Id = 2;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/sb-core1.png', SINGLE_BLOB) AS img
)
WHERE Id = 3;

-- Tiếp tục thêm các câu lệnh UPDATE cho các file ảnh khác với Id tương ứng
-- Ví dụ:
UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/sb-core2.png', SINGLE_BLOB) AS img
)
WHERE Id = 4;

-- Và cứ tiếp tục cho các file ảnh còn lại

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/sb-react1.png', SINGLE_BLOB) AS img
)
WHERE Id = 5;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/sb-ts1.png', SINGLE_BLOB) AS img
)
WHERE Id = 6;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/hat-core1.png', SINGLE_BLOB) AS img
)
WHERE Id = 7;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/hat-react1.png', SINGLE_BLOB) AS img
)
WHERE Id = 8;

-- Tiếp tục thêm các câu lệnh UPDATE cho các file ảnh khác với Id tương ứng
-- Ví dụ:
UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/hat-react2.png', SINGLE_BLOB) AS img
)
WHERE Id = 9;

-- Và cứ tiếp tục cho các file ảnh còn lại

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/glove-code1.png', SINGLE_BLOB) AS img
)
WHERE Id = 10;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/glove-code2.png', SINGLE_BLOB) AS img
)
WHERE Id = 11;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/glove-react1.png', SINGLE_BLOB) AS img
)
WHERE Id = 12;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/glove-react2.png', SINGLE_BLOB) AS img
)
WHERE Id = 13;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/boot-redis1.png', SINGLE_BLOB) AS img
)
WHERE Id = 14;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/boot-core2.png', SINGLE_BLOB) AS img
)
WHERE Id = 15;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/boot-core1.png', SINGLE_BLOB) AS img
)
WHERE Id = 16;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/boot-ang2.png', SINGLE_BLOB) AS img
)
WHERE Id = 17;

UPDATE Products
SET Picture = (
    SELECT BulkColumn
    FROM OPENROWSET(BULK 'C:/Users/HUY/source/angular_net/src/Server/API/Assets/images/products/boot-ang1.png', SINGLE_BLOB) AS img
)
WHERE Id = 18;

-- Và cứ tiếp tục cho các file ảnh còn lại
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
