SET NAMES utf8mb4;
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

USE time_clash_chronicles;

-- Inserts

-- Player dummy insert
INSERT INTO player (name) VALUES ('Player1');

-- Ability inserts
INSERT INTO ability (name, cost, type) VALUES
    ('Providencia', 0, 'Pasiva'),
    ('Agua bendita', 2, 'Activa'),
    ('Inspiración Literaria', 3, 'Activa'),
    ('Coman pastel!', 3, 'Activa'),
    ('Grito de Libertad', 0, 'Activa'),
    ('Embargo', 5, 'Activa'),
    ('Riqueza abrumadora', 0, 'Pasiva'),
    ('Línea de Ensamblaje', 0, 'Pasiva'),
    ('Medicina Ancestral', 1, 'Activa'),
    ('Corte eléctrico', 3, 'Activa'),
    ('Emancipación', 3, 'Activa'),
    ('Fuerza Rebelde', 3, 'Activa'),
    ('Camino Samurai', 0, 'Pasiva'),
    ('Camino del Perdedor', 0, 'Pasiva'),
    ('Renacimiento', 0, 'Pasiva'),
    ('Orgullo', 0, 'Pasiva');

INSERT INTO leader_ability (name, cost, cooldown) VALUES
    ('Embargo', 5, 3),
    ('Impuestos', 2, 4),
    ('Revolución', 4, 3),
    ('El Rey Demonio', 3, 2),
    ('Testudinum Formate', 4, 4);

-- Leader inserts
INSERT INTO leader (name, ability_name, ability_id) VALUES
    ('Napoleón', 'Embargo', 6),
    ('George Washington', 'Impuestos', 7),
    ('Emiliano Zapata', 'Revolución', 8),
    ('Oda Nobuna', 'El Rey Demonio', 9),
    ('Julio César', 'Testudinum Formate', 10);

-- Card inserts
INSERT INTO card (name, attack, health, basic_attack, cost, nation, leader_id, ability_id) VALUES
    ('Soldado de la Segunda Guerra Mundial', 2, 2, 'Tiro de Rifle', 2, 'Francia', 1, NULL),
    ('Caballero Francés', 4, 4, 'Corte de Espada', 4, 'Francia', 1, NULL),
    ('Juana de Arco', 8, 5, 'Espada Divina', 7, 'Francia', 1, 1),
    ('Revolucionario Francés', 2, 1, 'Grito de Libertad', 1, 'Francia', 1, NULL),
    ('Cura', 1, 4, 'Puño de Monje', 2, 'Francia', 1, 2),
    ('Victor Hugo', 2, 3, 'Pluma Asesina', 4, 'Francia', 1, 3),
    ('María Antonieta', 2, 5, 'Bofetón', 3, 'Francia', 1, 4),
    ('Soldado de la Independencia', 3, 3, 'Disparo de Mosquete', 3, 'Estados Unidos', 2, NULL),
    ('Abraham Lincoln', 3, 4, 'Discurso', 5, 'Estados Unidos', 2, 11),
    ('Marine', 4, 4, 'Ráfaga', 4, 'Estados Unidos', 2, NULL),
    ('John D. Rockefeller', 6, 9, 'Martillo de Oro', 9, 'Estados Unidos', 2, 7),
    ('Henry Ford', 3, 6, 'Golpe de llave', 6, 'Estados Unidos', 2, NULL),
    ('Benjamin Franklin', 4, 5, 'Relámpago', 5, 'Estados Unidos', 2, 10),
    ('Médico de la Guerra Civil', 2, 3, 'Bisturí', 3, 'Estados Unidos', 2, 9),
    ('Soldado Revolucionario', 2, 2, 'Disparo de Pistola', 2, 'México', 3, NULL),
    ('Charro', 3, 1, 'Lazo Vaquero', 2, 'México', 3, 2),
    ('Guerrero Azteca', 5, 4, 'Golpe de Macuahuitl', 4, 'México', 3, 3),
    ('Moctezuma', 6, 5, 'Lanza Azteca', 6, 'México', 3, 1),
    ('Quetzalcóatl', 7, 7, 'Serpiente Emplumada', 7, 'México', 3, 5),
    ('Benito Juarez', 5, 5, 'Golpe independentista', 5, 'México', 3, 11),
    ('Shaman', 2, 3, 'Arco y Flecha', 3, 'México', 3, 9),
    ('Samurai', 4, 2, 'Corte de Katana', 3, 'Japón', 4, NULL),
    ('Ninja', 2, 2, 'Shuriken', 2, 'Japón', 4, NULL),
    ('Sakamoto Ryoma', 5, 4, 'Espada Divina', 5, 'Japón', 4, 13),
    ('Soldado Imperial', 5, 3, 'Disparo de rifle', 4, 'Japón', 4, NULL),
    ('Geisha', 3, 1, 'Abanico', 2, 'Japón', 4, 14),
    ('Miyamoto Musashi', 6, 8, 'Katana Doble', 7, 'Japón', 4, 15),
    ('Sasaki Kojiro', 5, 9, 'Corte del agua', 7, 'Japón', 4, 16),
    ('Legionario', 1, 3, 'Ataque de Lanza', 2, 'Italia', 5, NULL),
    ('Centurión', 2, 4, 'Espadazo Romano', 3, 'Italia', 5, NULL),
    ('Diocles', 6, 8, 'Atropellamiento', 7, 'Italia', 5, 17),
    ('Gladiador', 3, 5, 'Hacha fulminante', 4, 'Italia', 5, NULL),
    ('Antiguos cristianos', 1, 4, 'Golpe de cruz', 3, 'Italia', 5, 2),
    ('Leonardo Da Vinci', 3, 6, 'Pincelazo', 5, 'Italia', 5, 16),
    ('Flamma', 7, 5, 'Corte Gladius', 6, 'Italia', 5, 18);

-- Deck inserts
INSERT INTO deck (name, description, nation, leader_id) VALUES
    ('Deck Francia', 'Deck de cartas de Francia', 'Francia', 1),
    ('Deck Estados Unidos', 'Deck de cartas de Estados Unidos', 'Estados Unidos', 2),
    ('Deck México', 'Deck de cartas de México', 'México', 3),
    ('Deck Japón', 'Deck de cartas de Japón', 'Japón', 4),
    ('Deck Italia', 'Deck de cartas de Italia', 'Italia', 5);

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;