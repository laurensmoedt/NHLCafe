DROP DATABASE IF EXISTS NHLCafeDB;
CREATE DATABASE IF NOT EXISTS NHLCafeDB;

USE NHLCafeDB;

# DROP TABLE IF EXISTS Category, Product CASCADE;
CREATE TABLE IF NOT EXISTS Account
(
    AccountId INT NOT NULL AUTO_INCREMENT,
    Email NVARCHAR(128) NOT NULL UNIQUE,
    Username NVARCHAR(128) NOT NULL,
    Password NVARCHAR(128) NOT NULL,
    
    CONSTRAINT PK_Account PRIMARY KEY (AccountId)
);

CREATE TABLE IF NOT EXISTS Category
(
    CategoryId INT NOT NULL AUTO_INCREMENT ,
    Name varchar(128) NOT NULL  UNIQUE,

    CONSTRAINT PK_Category_CategoryId PRIMARY KEY (CategoryId)
);

CREATE TABLE IF NOT EXISTS Product
(
    ProductId INT NOT NULL AUTO_INCREMENT,
    CategoryId INT NOT NULL,
    productName NVARCHAR(128) NOT NULL,
    Description NVARCHAR(256),
    Price DECIMAL(5, 2) NOT NULL,

    CONSTRAINT PK_Product_ProductId PRIMARY KEY (ProductId),
    CONSTRAINT FK_ProductCategory FOREIGN KEY (CategoryId) REFERENCES Category (CategoryId)
);

INSERT INTO Account(email, username, password)
VALUES ('laurensmoedt@gmail.com', 'laurens', 'nieuwwachtwoord');

INSERT INTO category (name) VALUES ('Frisdrank');
INSERT INTO category (name) VALUES ('Bier');
INSERT INTO category (name) VALUES ('Wijn');
INSERT INTO category (name) VALUES ('Koffie');

INSERT INTO product (productName, description, price, categoryid)
VALUES ('Cola', 'Coca-Cola', 100,
        (SELECT categoryid FROM category WHERE category.name = 'Frisdrank')
       );

INSERT INTO product (productName, description, price, categoryid)
VALUES ('Sinas', 'Fanta', 80,
        (SELECT categoryid FROM category WHERE category.name = 'Frisdrank')
       );

INSERT INTO product (productName, description, price, categoryid)
VALUES ('Heineken', '0.5L', 100,
        (SELECT categoryid FROM category WHERE category.name = 'Bier')
       );

INSERT INTO product (productName, description, price, categoryid)
VALUES ('Amstel', '0.5L', 80,
        (SELECT categoryid FROM category WHERE category.name = 'Bier')
       );

INSERT INTO product (productName, description, price, categoryid)
VALUES ('La Bestia Governo', 'Vol en Kragtige Merlot', 100,
        (SELECT categoryid FROM category WHERE category.name = 'Wijn')
       );

INSERT INTO product (productName, description, price, categoryid)
VALUES ('Acantus', 'Stevig, zacht en romige Tempranillo', 80,
        (SELECT categoryid FROM category WHERE category.name = 'Wijn')
       );

INSERT INTO product (productName, description, price, categoryid)
VALUES ('Cappuccino', 'Espresso, gestoomde melk en melkschuim', 80,
        (SELECT categoryid FROM category WHERE category.name = 'Koffie')
       );

INSERT INTO product (productName, description, price, categoryid)
VALUES ('Espresso', 'Sterk en geconcentreerd', 80,
        (SELECT categoryid FROM category WHERE category.name = 'Koffie')
       );

# JOIN Product with Categories to load all products and related category (see ProductRepository.GetProductWithCategories() method)
SELECT p.ProductId, p.productName, p.Description, p.Price, p.CategoryId, c.CategoryId, c.Name
FROM Product p JOIN Category c on c.CategoryId = p.CategoryId;

