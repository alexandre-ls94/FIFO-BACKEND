USE Fifo;

INSERT INTO Atividade (Titulo)
VALUES ('Sinuca');

INSERT INTO Usuario (Nickname)
VALUES ('Ale');

INSERT INTO Fila (Estado, CreatedAt, IdAtividade, IdUsuario)
VALUES ('Em Fila', '06-02-2020', 1, 1);

UPDATE Usuario
SET Senha = 'senha123'
WHERE Id = 1;

UPDATE Atividade
SET JogadoresPorVez = 2
WHERE Id = 1;
