CREATE DATABASE Fifo;

USE Fifo;

CREATE TABLE Atividade (
	Id INT PRIMARY KEY IDENTITY,
	Titulo VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Usuario (
	Id INT PRIMARY KEY IDENTITY,
	Nickname VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Fila (
	Id INT PRIMARY KEY IDENTITY,
	Estado VARCHAR(255),
	CreatedAt DATETIME2,
	IdAtividade INT FOREIGN KEY REFERENCES Atividade(Id),
	IdUsuario INT FOREIGN KEY REFERENCES Usuario(Id)
);