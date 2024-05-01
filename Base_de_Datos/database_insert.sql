SET NAMES utf8mb4;
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';
SET @old_autocommit=@@autocommit;

USE time_clash_chronicles;

INSERT INTO card (card_id, name, attack, health, basic_attack, cost, nation, leader_id, ability_id, deck_id) VALUES
    (1, 'Soldado ', 2, 2, 'Tiro de Rifle', 2, 'Francia', 1, NULL, 1),
    (2, 'Caballero Francés', 4, 4, 'Corte de Espada', 4, 'Francia', 1, NULL, 1),
    (3, 'Juana de Arco', 8, 5, 'Espada Divina', 7, 'Francia', 1, 1, 1),
    (4, 'Civil Francés', 2, 1, 'Grito de Libertad', 1, 'Francia', 1, 1, 1),
    (5, 'Cura', 1, 4, 'Puño de Monje', 2, 'Francia', 1, NULL, 1),
    (6, 'Victor Hugo', 2, 3, 'Pluma Asesina', 4, 'Francia', 1, 1, 1),
    (7, 'María Antonieta', 2, 5, 'Bofetón', 3, 'Francia', 1, NULL, 1),
    (8, 'Soldado', 3, 3, 'Disparo de Mosquete', 3, 'Estados Unidos', 2, NULL, 2),
    (9, 'Abraham Lincoln', 3, 4, 'Discurso', 5, 'Estados Unidos', 2, 2, 2),
    (10, 'Marine', 4, 4, 'Ráfaga', 4, 'Estados Unidos', 2, 2, 2),
    (11, 'John Rockefeller', 6, 9, 'Martillo de Oro', 9, 'Estados Unidos', 2, 2, 2),
    (12, 'Henry Ford', 3, 6, 'Golpe de llave', 6, 'Estados Unidos', 2, NULL, 2),
    (13, 'Benjamin Franklin', 4, 5, 'Relámpago', 5, 'Estados Unidos', 2, NULL, 2),
    (14, 'Médico', 2, 3, 'Bisturí', 3, 'Estados Unidos', 2, NULL, 2),
    (15, 'Soldado', 2, 2, 'Disparo de Pistola', 2, 'México', 3, NULL, 3),
    (16, 'Charro', 3, 1, 'Lazo Vaquero', 2, 'México', 3, 3, 3),
    (17, 'Guerrero Azteca', 5, 4, 'Golpe de Macuahuitl', 4, 'México', 3, NULL, 3),
    (18, 'Moctezuma', 6, 5, 'Lanza Azteca', 6, 'México', 3, NULL, 3),
    (19, 'Quetzalcóatl', 7, 7, 'Serpiente Emplumada', 7, 'México', 3, 3, 3),
    (20, 'Benito Juarez', 5, 5, 'Golpe independentista', 5, 'México', 3, 3, 3),
    (21, 'Shaman', 2, 3, 'Arco y Flecha', 3, 'México', 3, NULL, 3),
    (22, 'Samurai', 4, 2, 'Corte de Katana', 3, 'Japón', 4, NULL, 4),
    (23, 'Ninja', 2, 2, 'Shuriken', 2, 'Japón', 4, NULL, 4),
    (24, 'Sakamoto Ryoma', 5, 4, 'Espada Divina', 5, 'Japón', 4, 4, 4),
    (25, 'Soldado Imperial', 5, 3, 'Disparo de rifle', 4, 'Japón', 4, NULL, 4),
    (26, 'Geisha', 3, 1, 'Abanico', 2, 'Japón', 4, 4, 4),
    (27, 'Miyamoto Musashi', 6, 8, 'Katana Doble', 7, 'Japón', 4, 4, 4),
    (28, 'Sasaki Kojiro', 5, 9, 'Corte del agua', 7, 'Japón', 4, NULL, 4),
    (29, 'Legionario', 1, 3, 'Ataque de Lanza', 2, 'Italia', 5, NULL, 5),
    (30, 'Centurión', 2, 4, 'Espadazo Romano', 3, 'Italia', 5, NULL, 5),
    (31, 'Diocles', 6, 8, 'Atropellamiento', 7, 'Italia', 5, 5, 5),
    (32, 'Gladiador', 3, 5, 'Hacha fulminante', 4, 'Italia', 5, NULL, 5),
    (33, 'Antiguos cristianos', 1, 4, 'Golpe de cruz', 3, 'Italia', 5, 5, 5),
    (34, 'Leo DaVinci', 3, 6, 'Pincelazo', 5, 'Italia', 5, 5, 5),
    (35, 'Flamma', 7, 5, 'Corte Gladius', 6, 'Italia', 5, NULL, 5);

INSERT INTO deck (deck_id, name, description, nation) VALUES
    (1, 'Deck Francia', 'Deck de cartas de Francia', 'Francia'),
    (2, 'Deck Estados Unidos', 'Deck de cartas de Estados Unidos', 'Estados Unidos'),
    (3, 'Deck México', 'Deck de cartas de México', 'México'),
    (4, 'Deck Japón', 'Deck de cartas de Japón', 'Japón'),
    (5, 'Deck Italia', 'Deck de cartas de Italia', 'Italia');


INSERT INTO player (player_id, username, password, wins, loses) VALUES
(1, 'Diego', "123", 1, 2),
(2, 'Andres', "123", 3, 1),
(3, 'Isaac', "123", 2, 3);


INSERT INTO leader (leader_id, name, deck_id) VALUES
    (1, 'Napoleón', 1),
    (2, 'George Washington',  2),
    (3, 'Emiliano Zapata', 3),
    (4, 'Oda Nobuna', 4),
    (5, 'Julio César',  5);

INSERT INTO ability (ability_id, name, description, cost, type) VALUES

    (5, 'Curar','Cura a tus cartas aliadas {value} de vida' , 0, 'activa'),
    (3, 'Incrementa Daño','Incrementa en {value} el ataque de tus cartas aliadas' , 0, 'activa'),
    (2, 'Reducir Daño','Reduce {value} del ataque de las cartas enemigas' , 0, 'activa'),
    (4, 'Robar Vida','Roba {value} de vida por cada carta enemiga en la arena.' , 0, 'activa'),
    (1, 'Atacar Lider','Hace {value} de daño directamente al lider enemigo ' , 0, 'activa');
  



INSERT INTO game (player_id, win, deck_id) VALUES
    (1,0,2),
    (1,0,3),
    (2,0,4),
    (2,1,5),
    (3,0,1),
    (1,1,4),
    (3,0,4),
    (1,1,3),
    (3,1,5),
    (2,0,2);
SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
SET autocommit=@old_autocommit;