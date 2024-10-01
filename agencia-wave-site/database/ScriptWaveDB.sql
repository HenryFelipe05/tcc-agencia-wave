-- Criação do banco de dados
CREATE DATABASE waveDB;
GO

USE waveDB;
GO

-- Tabela Genero
CREATE TABLE Genero (
    CodigoGenero INT PRIMARY KEY IDENTITY,
    DescricaoGenero VARCHAR(50) NOT NULL,
    Ativo BIT NOT NULL,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabela TipoPessoa
CREATE TABLE TipoPessoa (
    CodigoTipoPessoa INT PRIMARY KEY IDENTITY,
    Descricao VARCHAR(100) NOT NULL
);

-- Tabela Pessoa
CREATE TABLE Pessoa (
    CodigoPessoa INT PRIMARY KEY IDENTITY,
    Nome VARCHAR(100) NOT NULL,
    Sobrenome VARCHAR(100),
    Email VARCHAR(100) UNIQUE,
    Telefone VARCHAR(20),
    Documento VARCHAR(20),
    DataNascimento DATE,
    CodigoGenero INT FOREIGN KEY REFERENCES Genero(CodigoGenero),
    CodigoTipoPessoa INT FOREIGN KEY REFERENCES TipoPessoa(CodigoTipoPessoa)
);

-- Tabela Endereco
CREATE TABLE Endereco (
    CodigoEndereco INT PRIMARY KEY IDENTITY,
    Pais VARCHAR(50),
    Estado VARCHAR(50),
    Cidade VARCHAR(50),
    Bairro VARCHAR(50),
    CEP VARCHAR(20),
    Ativo BIT NOT NULL,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabela EnderecoPessoa
CREATE TABLE EnderecoPessoa (
    CodigoEnderecoPessoa INT PRIMARY KEY IDENTITY,
    CodigoEndereco INT FOREIGN KEY REFERENCES Endereco(CodigoEndereco),
    CodigoPessoa INT FOREIGN KEY REFERENCES Pessoa(CodigoPessoa)
);

-- Tabela Perfil
CREATE TABLE Perfil (
    CodigoPerfil INT PRIMARY KEY IDENTITY,
    DescricaoPerfil VARCHAR(50),
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabela Usuario
CREATE TABLE Usuario (
    CodigoUsuario INT PRIMARY KEY IDENTITY,
    NomeUsuario VARCHAR(50) UNIQUE NOT NULL,
    Senha VARCHAR(255) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Ativo BIT NOT NULL,
    CodigoPerfil INT FOREIGN KEY REFERENCES Perfil(CodigoPerfil),
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
    Titulo VARCHAR(255),
    ExtensaoArquivo VARCHAR(10),
    URLMiniatura VARCHAR(255),
    Ativo BIT NOT NULL,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabela Favorito
CREATE TABLE Favorito (
    CodigoFavorito INT PRIMARY KEY IDENTITY,
    CodigoUsuario INT FOREIGN KEY REFERENCES Usuario(CodigoUsuario),
    CodigoItemGaleria INT FOREIGN KEY REFERENCES ItemGaleria(CodigoItemGaleria),
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
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
    Preco DECIMAL(18, 2) NOT NULL,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabela Assinatura
CREATE TABLE Assinatura (
    CodigoAssinatura INT PRIMARY KEY IDENTITY,
    CodigoTipoAssinatura INT FOREIGN KEY REFERENCES TipoAssinatura(CodigoTipoAssinatura),
    CodigoUsuario INT FOREIGN KEY REFERENCES Usuario(CodigoUsuario),
    CodigoStatusAssinatura INT FOREIGN KEY REFERENCES StatusAssinatura(CodigoStatusAssinatura),
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabela Contato
CREATE TABLE Contato (
    CodigoContato INT PRIMARY KEY IDENTITY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Mensagem VARCHAR(MAX),
    DataContato DATETIME NOT NULL DEFAULT GETDATE(),
    CodigoStatusContato INT FOREIGN KEY REFERENCES StatusAssinatura(CodigoStatusAssinatura)
);

-- Tabela SituacaoContato
CREATE TABLE SituacaoContato (
    CodigoStatusContato INT PRIMARY KEY IDENTITY,
    Descricao VARCHAR(100) NOT NULL
);
