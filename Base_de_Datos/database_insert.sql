SET NAMES utf8mb4;
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';
SET @old_autocommit=@@autocommit;

USE time_clash_chronicles;

INSERT INTO card (card_id, name, description, attack, health, cost, is_leader) VALUES 
(1, "Soldado de la Segunda Guerra Mundial", "Valiente soldado de la Segunda Guerra Mundial.", 2, 2, 2, 0),
(2, "Caballero Francés", "Noble defensor del honor de Francia.", 4, 4, 4, 0),
(3, "Juana de Arco", "Legendaria heroína guiada por la fe.", 8, 5, 7, 0),
(4, "Revolucionario Francés", "Audaz luchador por la libertad del pueblo.", 2, 1, 1, 0),
(5, "Cura", "Sanador dedicado a proteger a sus compañeros.", 1, 4, 2, 0),
(6, "Victor Hugo", "Escritor cuyas palabras inspiran coraje.", 2, 3, 4, 0),
(7, "María Antonieta", "Reina valiente que enfrenta la adversidad con gracia.", 2, 5, 3, 0),
(8, "Napoleón", "Emperador que marcó la historia con su genio militar.", 0, 0, 0, 1),
(9, "George Washington", "Primer presidente de EE. UU., conocido por su liderazgo en la guerra de independencia.", 0, 0, 0, 1),
(10, "Soldado de la Independencia", "Valiente luchador por la independencia de EE. UU.", 3, 3, 3, 0),
(11, "Abraham Lincoln", "Decimosexto presidente de EE. UU., líder durante la Guerra Civil.", 3, 4, 5, 0),
(12, "Marine", "Miembro de élite del cuerpo de marines de EE. UU.", 4, 4, 4, 0),
(13, "John D. Rockefeller", "Influente empresario conocido por su riqueza.", 6, 9, 9, 0),
(14, "Henry Ford", "Fundador de la Ford Motor Company, pionero en la producción en masa de automóviles.", 3, 6, 6, 0),
(15, "Benjamin Franklin", "Científico, inventor y político estadounidense.", 4, 5, 5, 0),
(16, "Soldado Revolucionario", "Soldado valiente que lucha por la causa revolucionaria.", 2, 2, 2, 0),
(17, "Charro", "Hábil jinete experto en el manejo del lazo vaquero.", 3, 1, 2, 0),
(18, "Guerrero Azteca", "Feroz guerrero imbuido con el espíritu guerrero de los antiguos aztecas.", 5, 4, 4, 0),
(19, "Moctezuma", "Emperador azteca que lidera con ferocidad en el campo de batalla.", 6, 5, 6, 0),
(20, "Quetzalcóatl", "Poderosa deidad azteca representada como una serpiente emplumada.", 7, 7, 7, 0),
(21, "Benito Juárez", "Inspirador líder de la independencia mexicana, decidido a luchar por la libertad.", 5, 5, 5, 0),
(22, "Shaman", "Hechicero sabio que canaliza la medicina ancestral para curar y proteger a sus aliados.", 2, 3, 3, 0),
(23, "Emiliano Zapata", "Líder valiente y carismático de la Revolución Mexicana", 0, 0, 0, 1),
(24, "Samurai", "Guerrero honorable entrenado en las artes del bushido, cuya espada corta el aire con precisión mortal.", 4, 2, 3, 0),
(25, "Ninja", "Maestro sigiloso de las sombras, cuyos shurikens encuentran su blanco con letal precisión.", 2, 2, 2, 0),
(26, "Sakamoto Ryoma", "Revolucionario valiente y visionario, cuya espada defiende los ideales de un nuevo Japón.", 5, 4, 5, 0),
(27, "Soldado Imperial", "Soldado leal al emperador, cuya precisión con el rifle es temida en todo el campo de batalla.", 5, 3, 4, 0),
(28, "Geisha", "Artista femenina entrenada en las artes de la seducción y el combate, cuyo abanico es tan letal como hermoso.", 3, 1, 2, 0),
(29, "Miyamoto Musashi", "Legendario espadachín ronin, cuyas habilidades con la katana son incomparables en todo el Japón.", 6, 8, 7, 0),
(30, "Sasaki Kojiro", "Esgrimista hábil y despiadado, cuya técnica de la espada corta como el viento y hiere como el agua.", 5, 9, 7, 0);


INSERT INTO player (player_id, name, wins, loses) VALUES
(1, 'Diego', 3, 0),
(2, 'Sofía', 5, 2),
(3, 'Alejandro', 1, 1),
(4, 'María', 2, 3),
(5, 'Juan', 4, 2),
(6, 'Ana', 6, 1),
(7, 'Carlos', 2, 2),
(8, 'Laura', 3, 4),
(9, 'Daniel', 5, 3),
(10, 'Lucía', 1, 0),
(11, 'Pedro', 3, 2),
(12, 'Paula', 2, 1),
(13, 'Miguel', 4, 3),
(14, 'Isabella', 1, 2),
(15, 'Manuel', 3, 3),
(16, 'Gabriela', 2, 1),
(17, 'Ricardo', 4, 1),
(18, 'Fernanda', 3, 2),
(19, 'Hernán', 1, 4),
(20, 'Camila', 5, 3),
(21, 'Andrés', 2, 2),
(22, 'Valentina', 3, 1),
(23, 'José', 4, 3),
(24, 'Carolina', 2, 0),
(25, 'Roberto', 3, 3),
(26, 'Verónica', 1, 2),
(27, 'Federico', 2, 1),
(28, 'Patricia', 5, 4),
(29, 'Gonzalo', 3, 2),
(30, 'Natalia', 4, 1);

INSERT INTO deck (deck_id, name, description, nation) VALUES
(1, "Legión de los Augures", "Una fuerza mística del antiguo Imperio Romano, cuyas predicciones guían su camino hacia la victoria.", "Italia"),
(2, "Furia Vikinga", "Una horda indomable de guerreros del norte, cuyos rugidos de batalla sacuden los cimientos del mundo.", "Escandinavia"),
(3, "Sombra del Shogun", "Guerreros samuráis entrenados en las artes marciales más oscuras, protegiendo con honor el honor del Japón feudal.", "Japón"),
(4, "Falange Espartana", "Una formación militar impenetrable de guerreros espartanos, cuyas lanzas son como un muro de bronce.", "Grecia"),
(5, "Orden de la Rosa", "Caballeros valientes y nobles, cuyas espadas defienden la justicia con la fuerza de mil rosas en flor.", "Europa"),
(6, "Leyenda Conquistadora", "Exploradores y aventureros audaces, cuyo destino es forjar un nuevo mundo a través de la conquista y la exploración.", "España"),
(7, "Clan de las Sombras", "Ninjas expertos en las artes del sigilo y el asesinato, cuya presencia es como un susurro en la noche.", "Japón"),
(8, "Guardia Galorromana", "Legionarios valientes de la antigua Galia, cuya ferocidad en el campo de batalla es tan feroz como el rugido de un león.", "Francia"),
(9, "Guardianes del Nilo", "Soldados del antiguo Egipto, cuyos escudos protegen los secretos de las pirámides y los tesoros del Nilo.", "Egipto"),
(10, "Horda Mongol", "Jinetes y arqueros de las vastas estepas de Mongolia, cuya velocidad y precisión son legendarias en todo el mundo conocido.", "Mongolia"),
(11, "Calavera Caribeña", "Piratas astutos y temibles, cuyas calaveras adornan las velas de sus barcos mientras navegan hacia la riqueza y la gloria.", "Caribe"),
(12, "Muralla de Jade", "Defensores valientes de la Gran Muralla China, cuya resistencia es tan fuerte como el jade que protegen.", "China"),
(13, "Orden del Grial", "Caballeros templarios que buscan reliquias sagradas, cuyo valor es tan grande como su devoción a la fe.", "Europa"),
(14, "Ejército de Quetzalcóatl", "Guerreros aztecas bendecidos por los dioses, cuyo coraje es tan grande como las serpientes emplumadas que los protegen.", "Mesoamérica"),
(15, "Clan de los Cuervos", "Guerreros celtas de las tierras altas, cuyos gritos de batalla son como el graznido de los cuervos en el viento.", "Celtas"),
(16, "Caballeros de la Luna", "Guerreros nocturnos imbuidos de la magia de la luna, cuyos ataques son tan rápidos como la luz plateada.", "Inglaterra"),
(17, "Guardianes del Cielo", "Protectores celestiales dotados de alas resplandecientes, cuyo deber es mantener la paz en los reinos celestiales.", "Francia"),
(18, "Orden del Dragón", "Caballeros que han jurado proteger los antiguos tesoros custodiados por dragones, con valentía y honor hasta la muerte.", "China"),
(19, "Llama del Fénix", "Una legión ardiente de guerreros envueltos en llamas de renacimiento, que renacen de sus cenizas para luchar una y otra vez.", "Japón"),
(20, "Legión del Trueno", "Soldados imbuidos del poder del rayo y el trueno, cuyos rugidos en el campo de batalla son como el eco de los dioses.", "Nórdico"),
(21, "Tribu del Lobo", "Guerreros de la naturaleza, en armonía con los lobos y la tierra salvaje, cuyo aullido en la batalla es tan feroz como el de sus aliados.", "Escandinavia"),
(22, "Cofradía del Viento", "Maestros del viento y la velocidad, cuyos ataques son tan rápidos como el susurro del viento y tan mortales como un huracán.", "Arabia"),
(23, "Orden del Oso", "Guerreros imponentes y poderosos, que canalizan la fuerza del oso en el campo de batalla, derribando a sus enemigos con fuerza bruta.", "Rusia"),
(24, "Hijos de la Oscuridad", "Criaturas de la noche envueltas en sombras, cuyos ojos brillan con una malicia siniestra y un deseo de destrucción.", "Alemania"),
(25, "Reino de las Hadas", "Serena magia y encanto se encuentran en este reino de hadas, donde las criaturas mágicas luchan con gracia y poder deslumbrante.", "Irlanda"),
(26, "Guardia del Abismo", "Guerreros valientes que protegen los límites del abismo, luchando contra las fuerzas del caos y la destrucción con una determinación inquebrantable.", "Austria"),
(27, "Cruzados del Sol", "Caballeros sagrados que luchan en nombre del sol, su armadura resplandece con la luz divina mientras purifican el campo de batalla de la oscuridad.", "España"),
(28, "Hijos de la Aurora", "Criaturas místicas nacidas de la luz de la aurora, cuya belleza y gracia en el campo de batalla son tan deslumbrantes como su poder.", "Suecia"),
(29, "Orden del Águila", "Guerreros ágiles y veloces que surcan los cielos con las alas del águila, protegiendo los cielos de cualquier amenaza que se cierna sobre ellos.", "Estados Unidos"),
(30, "Hijos del Dragón", "Descendientes de dragones antiguos, cuyo fuego ardiente y escamas resistentes los convierten en una fuerza imparable en el campo de batalla.", "Japón");

INSERT INTO arena (arena_id, name, nation) VALUES
(1, "Coliseo Romano", "Italia"),
(2, "Akershus Fortress", "Noruega"),
(3, "Kiyomizu-dera", "Japón"),
(4, "Esparta", "Grecia"),
(5, "Stonehenge", "Reino Unido"),
(6, "Alhambra", "España"),
(7, "Versalles", "Francia"),
(8, "Templo de Karnak", "Egipto"),
(9, "Estepas de Mongolia", "Mongolia"),
(10, "Chichen Itza", "México"),
(11, "Muralla China", "China"),
(12, "Castillo de Neuschwanstein", "Alemania"),
(13, "Machu Picchu", "Perú"),
(14, "Gran Pirámide de Giza", "Egipto"),
(15, "Monte Fuji", "Japón"),
(16, "Taj Mahal", "India"),
(17, "Monte Everest", "Nepal"),
(18, "Opera House de Sydney", "Australia"),
(19, "Monte Rushmore", "Estados Unidos"),
(20, "Petra", "Jordania"),
(21, "Coliseo de Los Ángeles", "Estados Unidos"),
(22, "Monte Kilimanjaro", "Tanzania"),
(23, "Torre Eiffel", "Francia"),
(24, "Museo del Louvre", "Francia"),
(25, "Capilla Sixtina", "Vaticano"),
(26, "Monte Aconcagua", "Argentina"),
(27, "Puente Golden Gate", "Estados Unidos"),
(28, "Cristo Redentor", "Brasil"),
(29, "Gran Muralla de Kumbhalgarh", "India"),
(30, "Monte Elbrús", "Rusia");

INSERT INTO effect (effect_id, name, description, is_attack, damage, cost, is_leader_effect) VALUES
(1, "Providencia", "Al atacar directamente al líder contrario, se roba 1 moneda de oro.", 0, 0, 3, 0),
(2, "Agua Bendita", "Cura 2 puntos de vida de una carta seleccionada.", 0, 0, 2, 0),
(3, "Inspiración Literaria", "Una de tus cartas francesas obtiene un bono de 3 a su ataque y puntos de vida. Este efecto dura 3 turnos.", 0, 0, 3, 0),
(4, "¡Coman pastel!", "Selecciona una carta enemiga para evitar que ataque por un turno.", 0, 0, 3, 0),
(5, "Emancipación", "Reduce 3 puntos de ataque de una carta enemiga durante un turno.", 0, 0, 3, 0),
(6, "Riqueza abrumadora", "Rockefeller gana 1 de daño por cada 2 monedas de oro que posea el jugador. Máximo hasta 5 de daño.", 0, 0, 3, 0),
(7, "Línea de Ensamblaje", "Genera 2 de oro adicional por cada turno que esté Henry invocado en el campo.", 0, 0, 2, 0),
(8, "Disparo de Mosquete", "Inflige daño a una carta enemiga.", 1, 2, 1, 0),
(9, "Discurso", "Inflige daño a una carta enemiga.", 1, 0, 0, 0),
(10, "Ráfaga", "Inflige daño a una carta enemiga.", 1, 0, 0, 0),
(11, "Martillo de Oro", "Inflige daño a una carta enemiga.", 1, 0, 0, 0),
(12, "Golpe de Llave", "Inflige daño a una carta enemiga.", 1, 0, 0, 0),
(13, "Relámpago", "Inflige daño a una carta enemiga.", 1, 0, 0, 0),
(14, "Corte de Katana", "Inflige daño a una carta enemiga.", 1, 2, 3, 0),
(15, "Shuriken", "Inflige daño a una carta enemiga.", 1, 2, 2, 0),
(16, "Espada Divina", "Inflige daño a una carta enemiga.", 1, 3, 5, 0),
(17, "Disparo de rifle", "Inflige daño a una carta enemiga.", 1, 3, 4, 0),
(18, "Abanico", "Inflige daño a una carta enemiga.", 1, 3, 2, 0),
(19, "Katana Doble", "Inflige daño moderado a una carta enemiga.", 1, 4, 7, 0),
(20, "Corte del agua", "Inflige daño moderado a una carta enemiga.", 1, 4, 7, 0),
(21, "Fuerza Rebelde", "Otorga robo de vida de 2 puntos a una carta por los próximos 3 turnos.", 0, 0, 3, 0),
(22, "Masaje", "Cura 2 puntos de vida de una carta seleccionada.", 0, 0, 2, 0),
(23, "Camino Samurai", "Cada vez que Musashi destruye una carta enemiga, aumenta su daño en 1. Máximo de 5 de daño.", 0, 0, 0, 0),
(24, "Camino del Perdedor", "Cada vez que es atacado, Sasaki gana 2 puntos de ataque. Máximo de 6.", 0, 0, 3, 0),
(25, "El Rey Demonio", "Todas las cartas aliadas adquieren un robo de vida de 1 punto de vida por cada 2 puntos de ataque. Máximo de 5 puntos de vida.", 0, 0, 3, 1);

INSERT INTO deck_card (deck_card_id, quantity, card_id, deck_id) VALUES
(1, 3, 1, 1), 
(2, 2, 2, 1), 
(3, 1, 3, 1), 
(4, 3, 4, 2), 
(5, 2, 5, 2), 
(6, 1, 6, 2), 
(7, 3, 7, 3), 
(8, 2, 8, 3), 
(9, 1, 9, 3),
(10, 2, 24, 3),
(11, 2, 25, 3),
(12, 1, 26, 3),
(13, 1, 27, 3),
(14, 1, 28, 3),
(15, 1, 29, 3),
(16, 1, 30, 3),
(17, 2, 7, 4),
(18, 1, 24, 4),
(19, 2, 25, 4),
(20, 2, 26, 4),
(21, 1, 27, 4),
(22, 1, 28, 4),
(23, 1, 29, 4),
(24, 2, 30, 4),
(25, 2, 6, 5),
(26, 1, 21, 5),
(27, 2, 22, 5),
(28, 2, 23, 5),
(29, 1, 24, 5),
(30, 1, 25, 5);

INSERT INTO player_deck (player_deck_id, player_id, deck_id) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 3, 2),
(4, 4, 2),
(5, 5, 3),
(6, 6, 3),
(7, 7, 4),
(8, 8, 4),
(9, 9, 5),
(10, 10, 5),
(11, 11, 6),
(12, 12, 6),
(13, 13, 7),
(14, 14, 7),
(15, 15, 8),
(16, 1, 8),
(17, 2, 9),
(18, 3, 9),
(19, 4, 10),
(20, 5, 10),
(21, 6, 11),
(22, 7, 11),
(23, 8, 12),
(24, 9, 12),
(25, 10, 13),
(26, 11, 13),
(27, 12, 14),
(28, 13, 14),
(29, 14, 15),
(30, 15, 15);

INSERT INTO game (game_id, duration, player1_id, player2_id, winner_id, arena_id) VALUES 
(1, 300, 1, 2, 1, 1),
(2, 450, 3, 4, 3, 2),
(3, 600, 5, 6, 5, 3),
(4, 360, 7, 8, 8, 4),
(5, 420, 9, 10, 10, 5),
(6, 480, 11, 12, 11, 6),
(7, 540, 13, 14, 14, 7),
(8, 400, 15, 1, 1, 8),
(9, 300, 2, 3, 2, 9),
(10, 450, 4, 5, 4, 10),
(11, 600, 6, 7, 7, 11),
(12, 360, 8, 9, 8, 12),
(13, 420, 10, 11, 11, 13),
(14, 480, 12, 13, 12, 1),
(15, 540, 14, 15, 14, 2),
(16, 300, 1, 6, 1, 9),
(17, 450, 2, 7, 1, 10),
(18, 600, 3, 8, 2, 11),
(19, 360, 4, 9, 2, 12),
(20, 420, 5, 10, 1, 13),
(21, 480, 6, 11, 2, 14),
(22, 540, 7, 12, 1, 15),
(23, 400, 8, 13, 2, 16),
(24, 300, 9, 14, 1, 17),
(25, 450, 10, 15, 2, 18),
(26, 600, 11, 16, 1, 19),
(27, 360, 12, 17, 2, 20),
(28, 420, 13, 18, 1, 21),
(29, 480, 14, 19, 2, 22),
(30, 540, 15, 20, 1, 23);

INSERT INTO turn (turn_id, game_id, player_id, turn_number, phase) VALUES
(1, 1, 1, 1, "draw"),
(2, 1, 2, 2, "draw"),
(3, 3, 7, 3, "attack"),
(4, 1, 9, 4, "invoke"),
(5, 5, 3, 5, "draw"),
(6, 3, 5, 6, "draw"),
(7, 1, 12, 7, "invoke"),
(8, 6, 11, 8, "invoke"),
(9, 9, 13, 9, "invoke"),
(10, 12, 5, 10, "attack"),
(11, 13, 3, 11, "attack"),
(12, 7, 4, 12, "invoke"),
(13, 7, 7, 13, "draw"),
(14, 1, 2, 14, "draw"),
(15, 2, 1, 15, "invoke"),
(16, 2, 2, 16, "draw"),
(17, 6, 1, 17, "invoke"),
(18, 9, 2, 18, "invoke"),
(19, 9, 1, 19, "attack"),
(20, 14, 2, 20, "draw"),
(21, 15, 3, 1, "draw"),
(22, 2, 4, 2, "draw"),
(23, 3, 3, 3, "draw"),
(24, 2, 4, 4, "invoke"),
(25, 2, 3, 5, "invoke"),
(26, 2, 4, 6, "invoke");

INSERT INTO game_action (game_action_id, game_id, player_id, card_id, action_type, turn_id) VALUES
(1, 3, 4, 1, "attack", 3),
(2, 5, 6, 2, "effect", 5),
(3, 7, 8, 3, "attack", 7),
(4, 9, 10, 4, "invoke", 9),
(5, 11, 12, 5, "attack", 11),
(6, 13, 14, 6, "effect", 13),
(7, 5, 6, 2, "effect", 5),
(8, 7, 8, 3, "attack", 7),
(9, 9, 10, 4, "invoke", 9),
(10, 11, 12, 5, "attack", 11),
(11, 13, 14, 6, "effect", 13),
(12, 15, 16, 7, "attack", 15),
(13, 17, 18, 8, "effect", 17),
(14, 19, 20, 9, "attack", 19),
(15, 21, 22, 10, "invoke", 21),
(16, 23, 24, 11, "attack", 23),
(17, 25, 26, 12, "effect", 25),
(18, 27, 28, 13, "attack", 23),
(19, 29, 30, 14, "invoke", 24),
(20, 1, 2, 15, "attack", 1),
(21, 3, 4, 16, "effect", 3),
(22, 5, 6, 17, "attack", 5),
(23, 7, 8, 18, "invoke", 7),
(24, 9, 10, 19, "attack", 9),
(25, 11, 12, 20, "effect", 11),
(26, 13, 14, 21, "attack", 13),
(27, 15, 16, 22, "invoke", 15),
(28, 17, 18, 23, "attack", 17),
(29, 19, 20, 24, "effect", 19),
(30, 21, 22, 25, "attack", 21);

INSERT INTO game_card (game_card_id, player_id, game_id, card_id, status, position, instance_count)
VALUES
(1, 1, 1, 1, 'deck', 1, 2),
(2, 1, 1, 2, 'hand', 2, 3),
(3, 1, 1, 3, 'board', 3, 1),
(4, 1, 1, 4, 'discard_pile', 4, 2),
(5, 1, 1, 5, 'deck', 5, 1),
(6, 2, 1, 6, 'deck', 1, 1),
(7, 2, 1, 7, 'deck', 2, 2),
(8, 2, 1, 8, 'hand', 3, 1),
(9, 2, 1, 9, 'hand', 4, 2),
(10, 2, 1, 10, 'board', 5, 1),
(11, 3, 2, 11, 'deck', 1, 2),
(12, 3, 2, 12, 'hand', 2, 1),
(13, 3, 2, 13, 'deck', 3, 2),
(14, 3, 2, 14, 'discard_pile', 4, 1),
(15, 3, 2, 15, 'board', 5, 2),
(16, 4, 2, 16, 'deck', 1, 1),
(17, 4, 2, 17, 'deck', 2, 1),
(18, 4, 2, 18, 'hand', 3, 2),
(19, 4, 2, 19, 'hand', 4, 1),
(20, 4, 2, 20, 'board', 5, 2),
(21, 5, 3, 21, 'deck', 1, 1),
(22, 5, 3, 22, 'deck', 2, 2),
(23, 5, 3, 23, 'hand', 3, 1),
(24, 5, 3, 24, 'board', 4, 2),
(25, 5, 3, 25, 'discard_pile', 5, 1),
(26, 6, 3, 1, 'deck', 1, 2),
(27, 6, 3, 2, 'deck', 2, 3),
(28, 6, 3, 3, 'hand', 3, 1),
(29, 6, 3, 4, 'hand', 4, 2),
(30, 6, 3, 5, 'board', 5, 1);

INSERT INTO game_resources (game_resources_id, game_id, player_id, gold)
VALUES
(1, 1, 1, 5),
(2, 1, 2, 7),
(3, 2, 3, 3),
(4, 2, 4, 8),
(5, 3, 5, 2),
(6, 3, 6, 6),
(7, 4, 7, 4),
(8, 4, 8, 9),
(9, 5, 9, 3),
(10, 5, 10, 7),
(11, 6, 11, 6),
(12, 6, 12, 10),
(13, 7, 13, 2),
(14, 7, 14, 5),
(15, 8, 15, 9),
(16, 8, 1, 4),
(17, 9, 2, 8),
(18, 9, 3, 3),
(19, 10, 4, 7),
(20, 10, 5, 5),
(21, 11, 6, 6),
(22, 11, 7, 2),
(23, 12, 8, 9),
(24, 12, 9, 1),
(25, 13, 10, 8),
(26, 13, 11, 3),
(27, 14, 12, 6),
(28, 14, 13, 4),
(29, 15, 14, 5),
(30, 15, 15, 10);

INSERT INTO card_effect (card_effect_id, card_id, effect_id)
VALUES
(1, 1, 1),
(2, 2, 2),
(3, 3, 3),
(4, 4, 4),
(5, 5, 5),
(6, 6, 6),
(7, 7, 7),
(8, 8, 8),
(9, 9, 9),
(10, 10, 10),
(11, 11, 11),
(12, 12, 12),
(13, 13, 13),
(14, 14, 14),
(15, 15, 15),
(16, 16, 1),
(17, 17, 2),
(18, 18, 3),
(19, 19, 4),
(20, 20, 5),
(21, 21, 6),
(22, 22, 7),
(23, 23, 8),
(24, 24, 9),
(25, 25, 10),
(26, 26, 11),
(27, 27, 12),
(28, 28, 13),
(29, 29, 14),
(30, 30, 15);

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
SET autocommit=@old_autocommit;