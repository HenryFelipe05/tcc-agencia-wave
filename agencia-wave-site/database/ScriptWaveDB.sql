-- Criação do banco de dados
CREATE DATABASE waveDB;
GO

USE waveDB;
GO

-- Tabela Genero
CREATE TABLE Genero (
    CodigoGenero INT PRIMARY KEY IDENTITY,
    Descricao VARCHAR(50) NOT NULL,
);

-- Tabela Pessoa
CREATE TABLE Pessoa (
    CodigoPessoa INT PRIMARY KEY IDENTITY,
    Nome VARCHAR(100) NOT NULL,
    Sobrenome VARCHAR(100),
    DataNascimento DATE NULL,
    CodigoGenero INT FOREIGN KEY REFERENCES Genero(CodigoGenero) NULL,
);

-- Tabela Perfil
CREATE TABLE Perfil (
    CodigoPerfil INT PRIMARY KEY IDENTITY,
    Descricao VARCHAR(50)
);

-- Tabela Usuario
CREATE TABLE Usuario (
    CodigoUsuario INT PRIMARY KEY IDENTITY,
    NomeUsuario VARCHAR(50) UNIQUE NOT NULL,
	Email VARCHAR(100) UNIQUE NOT NULL,
	Telefone VARCHAR(20) NULL,
    Senha VARCHAR(255) NOT NULL,
    Ativo BIT NOT NULL,
    CodigoPerfil INT FOREIGN KEY REFERENCES Perfil(CodigoPerfil),
	CodigoPessoa INT FOREIGN KEY REFERENCES Pessoa(CodigoPessoa),
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabela Galeria
CREATE TABLE Galeria (
    CodigoGaleria INT PRIMARY KEY IDENTITY,
    Descricao VARCHAR(255) NOT NULL
);

-- Tabela ItemGaleria
CREATE TABLE ItemGaleria (
    CodigoItemGaleria INT PRIMARY KEY IDENTITY,
    CodigoGaleria INT FOREIGN KEY REFERENCES Galeria(CodigoGaleria),
    Titulo VARCHAR(255) NOT NULL,
	Descricao VARCHAR(255) NULL,
    ExtensaoArquivo VARCHAR(10) NOT NULL,
    Arquivo VARBINARY(MAX) NOT NULL,
    URLMiniatura VARCHAR(255) NOT NULL,
    Ativo BIT NOT NULL,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabela Favorito
CREATE TABLE Favorito (
    CodigoFavorito INT PRIMARY KEY IDENTITY,
    CodigoUsuario INT FOREIGN KEY REFERENCES Usuario(CodigoUsuario),
    CodigoItemGaleria INT FOREIGN KEY REFERENCES ItemGaleria(CodigoItemGaleria),
    DataFavorito DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabela StatusAssinatura
CREATE TABLE StatusAssinatura (
    CodigoStatusAssinatura INT PRIMARY KEY IDENTITY,
    Descricao VARCHAR(100) NOT NULL
);

-- Tabela TipoAssinatura
CREATE TABLE TipoAssinatura (
    CodigoTipoAssinatura INT PRIMARY KEY IDENTITY,
    Descricao VARCHAR(100) NOT NULL,
    Preco DECIMAL(18, 2) NOT NULL
);

-- Tabela Assinatura
CREATE TABLE Assinatura (
    CodigoAssinatura UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CodigoTipoAssinatura INT FOREIGN KEY REFERENCES TipoAssinatura(CodigoTipoAssinatura),
    CodigoUsuario INT FOREIGN KEY REFERENCES Usuario(CodigoUsuario),
    CodigoStatusAssinatura INT FOREIGN KEY REFERENCES StatusAssinatura(CodigoStatusAssinatura),
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
    DataCancelamento DATETIME NOT NULL DEFAULT GETDATE(),
    Ativa BIT NOT NULL
);

-- Tabela SituacaoContato
CREATE TABLE StatusContato (
    CodigoStatusContato INT PRIMARY KEY IDENTITY,
    Descricao VARCHAR(100) NOT NULL
);

-- Tabela Contato
CREATE TABLE Contato (
    CodigoContato INT PRIMARY KEY IDENTITY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Mensagem VARCHAR(MAX),
    DataContato DATETIME NOT NULL DEFAULT GETDATE(),
    CodigoStatusContato INT FOREIGN KEY REFERENCES StatusContato(CodigoStatusContato)
);

INSERT INTO Genero (Descricao) VALUES ('Masculino');
INSERT INTO Genero (Descricao) VALUES ('Feminino');
INSERT INTO Genero (Descricao) VALUES ('Não Binário');
INSERT INTO Genero (Descricao) VALUES ('Não Informar');

INSERT INTO Perfil (Descricao) VALUES ('Administrador');
INSERT INTO Perfil (Descricao) VALUES ('Operador');
INSERT INTO Perfil (Descricao) VALUES ('Usuario');
