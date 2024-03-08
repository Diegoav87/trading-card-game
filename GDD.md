# **Time Clash Chronicles**

## _Game Design Document_

---

##### Diego Abdo A01783808

##### Andres Gomes A01781321

##### Isaac Shakalo A01784045

##

## _Índice_

---

1. [Indíce](#index)
2. [Diseño del Juego](#game-design)
   1. [Resumen](#summary)
   2. [Gameplay](#gameplay)
   3. [Mindset](#mindset)
3. [Técnico](#technical)
   1. [Pantallas](#screens)
   2. [Controles](#controls)
   3. [Mecánicas](#mechanics)
4. [Diseño de Nivel](#level-design)
   1. [Arenas](#themes)
   2. [Flujo de Juego](#game-flow)
5. [Desarrollo](#development)
   1. [Clases Abstractas](#abstract-classes--components)
   2. [Clases Derivadas](#derived-classes--component-compositions)
6. [Gráficos](#graphics)
   1. [Atributos de Estilo](#style-attributes)
   2. [Graphicos Necesitados](#graphics-needed)
7. [Sonidos/Música](#soundsmusic)
   1. [Atributos de Estilo](#style-attributes-1)
   2. [Sonidos Requeridos](#sounds-needed)
   3. [Música](#music-needed)

## _Diseño del Juego_

---

### **Resumen**

Un juego de cartas donde personajes históricos batallan en una arena, siendo cada mazo representativo de un país, compuesto por los personajes más importantes de cada nación. Distintas naciones, tienen distintas fortalezas y debilidades, así como efectos especiales únicos de ese país y un líder representativo del mismo.

El objetivo del juego es bajar la vida del líder enemigo a 0 puntos de vida. Para conseguir este objetivo es necesario derrotar a su ejército, representado por el mazo. Cada carta cuenta con puntos de vida, ataque y costo, las cuales se enfrentan directamente para poder alcanzar al líder enemigo. Este juego tiene inspiración directa en juegos como Yu-gi-Oh y Pokemon TCG, tiene elementos de juegos de estrategia, TCG y Turn based combat.

### **Gameplay**

Cada jugador podrá elegir uno de los mazos disponibles al iniciar el juego, los cuales se encuentran ya prearmados dependiendo de su país. Una vez en la arena de juego se decide aleatoriamente que jugador inicia. Cada jugador empieza con tres cartas de su mazo que se dan de manera aleatoria y una cierta cantidad de monedas de oro. Al inicio de cada turno cada jugador toma una carta de su mazo y recibe 3 monedas de oro. Cada líder empieza con 4000 puntos de vida.

Cada carta tiene sus puntos de vida, ataque y costo, con esto el jugador puede decidir qué hacer en ese turno. El jugador puede invocar hasta 3 tres cartas según la cantidad de oro que tenga, las cartas invocadas solo pueden atacar hasta un turno después de ser invocada, a menos de que tengan un efecto que se los permita. Se puede atacar tanto a las cartas enemigas como al líder enemigo, sin embargo, no puedes atacar directamente al líder si existe al menos 1 carta enemiga en el campo.

Cuando una carta recibe un daño mayor o igual a los puntos de vida, es destruida y la diferencia entre el daño y la vida de la carta se le resta al líder enemigo. Una vez que los puntos de vida del líder enemigo llegan a 0, la partida termina y el jugador gana. Sin embargo, si el jugador pierde todos sus puntos de vida, es derrotado.

### **Mindset**

El juego será centrado en estrategia y marcado por lo visual y musical, el jugador debe adoptar un mindset de enfoque constante. La presión para tomar decisiones óptimas en cada turno es elevada, requiriendo concentración y análisis rápido.. La música y lo visual no solo añaden emoción, sino que también sirven como señales clave para anticipar momentos críticos. Este mindset permite al jugador sumergirse en la complejidad del juego y disfrutar plenamente de la experiencia.

## _Técnico_

---

### **Pantallas**

1. Menú de Inicio
   1. Dentro del menú de inicio se va a mostrar el logo y nombre del juego, habrá dos botones uno de iniciar el juego y otro de salir del juego.
   2. Al presionar inicio de juego tendrás que llenar tu nombre.
   3. Al presionar salir del juego se terminara el juego y saldras de el juego.
2. Selección de Baraja
   1. Al seleccionar la baraja la baraja elegida tendrá un efecto de resaltado para indicar que esa baraja está seleccionada y dará una descripción de qué es lo que hace la baraja.
3. Detalles de Baraja
   1. Dentro de la baraja se podrán ver todas las cartas que esta contiene con sus detalles.
4. Arena de Juego
   1. En la arena de juego, cada jugador dispondrá de tres posiciones, con una división en el centro. El jugador sostendrá en su mano tres cartas del deck, mientras que las restantes cartas del deck se ubicarán a la derecha de su mano. En la parte posterior de la arena, se situará el líder principal del país, y frente a él se desplegarán las oposiciones de las barajas que el jugador puede emplear.
5. Estadísticas
   1. En esta pantalla el jugador podrá ver estadísticas como su número de victorias y derrotas, número de juegos, etc.

### **Controles**

El juego se manejará con el mouse para poder hacer click en los botones necesarios.

### **Mecánicas**

1. Selección de mazo:

   1. Cada jugador elige uno de los mazos prearmados disponibles, que están vinculados a un país.

1. Juego:

   1. Se decide aleatoriamente qué jugador inicia.
   2. Cada jugador comienza con tres cartas aleatorias de su mazo.
   3. Cada jugador recibe una cierta cantidad de monedas de oro al inicio.
   4. Cada líder comienza con 4000 puntos de vida.

1. Invocación de Cartas:

   1. Al inicio de cada turno, cada jugador toma una carta de su mazo y recibe 3 monedas de oro.
   2. Los jugadores pueden invocar hasta 3 cartas por turno, dependiendo de la cantidad de oro que tengan.
   3. En la arena el jugador tiene 3 espacios para invocar cartas.
   4. Las cartas invocadas solo pueden atacar a partir del siguiente turno, a menos que tengan un efecto que lo permita.

1. Ataques:

   1. Los jugadores pueden atacar tanto a las cartas enemigas como al líder enemigo.
   2. No se puede atacar al líder si hay una carta enemiga directamente en frente de la carta.
   3. Cuando una carta recibe un daño igual o mayor a sus puntos de vida, se destruye.
   4. La diferencia entre el daño y la vida de la carta se resta de los puntos de vida del líder enemigo.
   5. La partida termina cuando los puntos de vida del líder enemigo llegan a 0, y el jugador gana.

1. Efectos:
   1. Ataques que infligen daño a cartas enemigas.
   2. Habilidades que reducen oro al oponente.
   3. Habilidades que le roban oro al oponente.
   4. Habilidades de curación.
   5. Habilidades de aumento de generación de oro por turno.
   6. Habilidades que reducen daño a cartas enemigas.
   7. Habilidades que aumentan el poder de ataque de cartas aliadas.
   8. Habilidades que permiten atacar varias veces por turno.
   9. Habilidades de ataques consecutivos.
   10. Habilidades de robo de vida.
   11. Habilidades de incremento de vida.

## _Diseño de Nivel_

---

### **Arenas**

1. Bosque de las Ardenas (Francia)

   1. Terreno con bosques y colinas.
   2. Efecto de niebla en la arena.
   3. Ambiente misterioso y oscuro.

2. Gran Cañón (Estados Unidos)

   1. Terreno desértico con acantilados.
   2. Vista lejana.
   3. Cambios de elevación.

3. Pirámides de Teotihuacán (México)

   1. Terreno de jungla.
   2. Estructuras y arquitectura.

4. Coliseo Romano (Italia)

   1. Arena circular.
   2. Arquitectura romana.

5. Monte Fuji (Japón)
   1. Terreno Montañoso.
   2. Ambiente nevado.

### **Cartas por Nación**

#### **Francia**

1. Líder: Napoleón
   - Habilidad Líder: Embargo - Se reduce a la mitad el oro del oponente. Mínimo 2 de oro.
2. Soldado de la Segunda Guerra Mundial
   - Ataque Básico: Tiro de Rifle - Hace una cantidad de daño a una carta enemiga.
3. Caballero Francés
   - Ataque Básico: Corte de Espada - Hace una cantidad de daño a una carta enemiga.
4. Juana de Arco
   - Habilidad especial: Providencia: Al atacar directamente al líder contrario, se roba 1 moneda de oro.
   - Ataque Básico: Espada Divina - Inflige daño a una carta enemiga.
5. Revolucionario Francés
   - Ataque Básico: Grito de Libertad - Inflige daño a una carta enemiga.
6. Cura
   - Ataque Básico: Evangelización - Inflige daño a una carta enemiga.
   - Habilidad Especial: Agua bendita - Cura 2 puntos de vida de una carta seleccionada.
7. Victor Hugo
   - Ataque Básico: Pluma Inspiradora - Inflige daño moderado a una carta enemiga.
   - Habilidad especial: Inspiración Literaria - Una de tus cartas francesas obtiene un bono de 3 a su ataque y puntos de vida.
8. María Antonieta
   - Ataque Básico: Bofetón - Inflige daño moderado a una carta enemiga.
   - Habilidad especial: ¡Coman pastel! - Selecciona una carta enemiga para evitar que ataque por un turno.

#### **Estados Unidos**

1. Líder: George Washington
   - Habilidad de Líder: Impuestos - Obtiene 1 moneda de oro por cada carta invocada en el campo de batalla.
2. Soldado de la Independencia
   - Ataque Básico: Disparo de Mosquete - Inflige daño a una carta enemiga.
3. Abraham Lincoln
   - Habilidad Especial: Emancipación - Reduce el poder de ataque de una carta enemiga en un 20% durante un turno.
   - Ataque Básico: Discurso - Inflige daño a una carta enemiga.
4. Marine
   - Ataque Básico: Ráfaga - Inflige daño a una carta enemiga.
5. John D. Rockefeller
   - Ataque Básico: Martillo de Oro - inflige daño a una carta enemiga.
   - Habilidad Especial: Riqueza abrumadora - Rockefeller gana 1 de daño por cada 2 monedas de oro que posea el jugador. Máximo hasta 5 de daño.
6. Henry Ford
   - Ataque Básico: Golpe de llave - inflige daño a una carta enemiga.
   - Habilidad Especial: Línea de Ensamblaje - Genera 2 de oro adicional por cada turno.
7. Benjamin Franklin
   - Ataque Básico: Relámpago - Inflige daño a una carta enemiga.
   - Habilidad Especial: Poder del Dolar - Si Benjamin Franklin logra atacar se da oro extra durante ese turno.
8. Médico de la Guerra Civil
   - Ataque Básico: Bisturí - Inflige daño a una carta enemiga.
   - Habilidad Especial: Rescate - Restaura una cantidad moderada de salud a una carta aliada.

#### **México**

1. Líder: Emiliano Zapata
   - Habilidad líder: Revolución - Todas las cartas invocadas en el campo adquieren un ataque adicional.
2. Soldado Revolucionario
   - Ataque Básico: Disparo de Pistola - Inflige daño a una carta enemiga.
3. Charro
   - Ataque Básico: Lazo Vaquero - Inflige daño a una carta enemiga.
4. Guerrero Azteca
   - Ataque Básico: Golpe de Macuahuitl - Inflige daño a una carta enemiga.
5. Moctezuma
   - Ataque Básico: Lanza Azteca - Inflige daño a una carta enemiga.
   - Habilidad especial: Fuerza del Imperio - Aumenta el poder de ataque de las cartas en el campo en 2 puntos.
6. Quetzalcóatl
   - Ataque Básico: Serpiente Emplumada - Inflige daño a una carta enemiga.
   - Habilidad Especial: Viento Divino: Cada vez que Quetzalcóatl ataque, se le añade un contador. Por cada contador, Quetzalcóatl obtiene 1 de daño.
7. Benito Juarez
   - Ataque Básico: Golpe independentista - Inflige daño a una carta enemiga.
   - Habilidad Especial: Grito revolucionario: Permite que una carta aliada ataque dos veces en un turno.
8. Shaman
   - Ataque Básico: Arco y Flecha - Inflige daño a una carta enemiga.
   - Habilidad Especial: Medicina Ancestral: Cura una carta aliada por 2 puntos.

#### **Japón**

1. Líder: Oda Nobunaga
   - Habilidad Líder: El Rey Demonio - Todas las cartas adquieren un robo de vida de 1 punto de vida por cada 2 puntos de ataque. Máximo de 5.
2. Samurai
   - Ataque Básico: Corte de Katana - Inflige daño a una carta enemiga.
3. Ninja
   - Ataque Básico: Shuriken - Inflige daño a una carta enemiga.
4. Sakamoto Ryoma
   - Habilidad especial: Fuerza Rebelde - le otorga robo de vida de 2 puntos a una carta por los próximos 3 turnos.
   - Ataque Básico: Espada Divina - Inflige daño a una carta enemiga.
5. Soldado Imperial
   - Ataque Básico: Disparo de rifle- Inflige daño a una carta enemiga.
6. Geisha
   - Ataque Básico: Abanico - Inflige daño a una carta enemiga.
   - Habilidad Especial: Masaje - Cura 2 puntos de vida de una carta seleccionada.
7. Miyamoto Musashi
   - Ataque Básico: Katana Doble- Inflige daño moderado a una carta enemiga.
   - Habilidad especial: Camino Samurai - Cada vez que Musashi destruye una carta enemiga, aumenta su daño en 1. Máximo de 5.
8. Sasaki Kojiro
   - Ataque Básico: Bofetón - Inflige daño moderado a una carta enemiga.
   - Habilidad especial: Camino del Perdedor - Cada vez que es atacado, Sasaki gana 2 puntos de ataque. Máximo de 6.

#### **Italia**

1. Líder: Julio César
   - Habilidad Líder: Testudinum Formate - Todas las cartas adquieren 3 puntos de vida.
2. Legionario
   - Ataque Básico: Ataque de Lanza - Inflige daño a una carta enemiga.
3. Centurión
   - Ataque Básico: Espadazo Romano - Inflige daño a una carta enemiga.
4. Diocles
   - Habilidad especial: Carrera Mortal - Si una carta ataca a Diocles, recibe 2 puntos de daño.
   - Ataque Básico: Atropellamiento - Inflige daño a una carta enemiga.
5. Gladiador
   - Ataque Básico: Hacha fulminante - Inflige daño a una carta enemiga.
6. Antiguos cristianos
   - Ataque Básico: Golpe de cruz - Inflige daño a una carta enemiga.
   - Habilidad Especial: Fé - Cura 2 puntos de vida de una carta seleccionada.
7. Leonardo Da Vinci
   - Ataque Básico: Pincelazo - Inflige daño moderado a una carta enemiga.
   - Habilidad especial: Renacimiento - Cada vez que Da Vinci destruye una carta enemiga, aumenta su daño en 1. Máximo de 5.
8. Flamma
   - Ataque Básico: Corte Gladius - Inflige daño moderado a una carta enemiga.
   - Habilidad especial: Orgullo - Flamma gana 1 punto de vida por cada carta enemiga destruida. Máximo 5.

### **Flujo de Juego**

1. Inicio de la Partida:

   - Se elige al azar un jugador para iniciar la partida.
   - Cada jugador comienza sin monedas, con 40 puntos de vida y 3 cartas en mano.

2. Turno del Jugador:

   - Al iniciar el turno, el jugador recibe 3 monedas de oro y toma una carta del mazo.
   - El jugador decide si invocar una o varias cartas según su cantidad de oro y el costo de las cartas.
   - Las cartas invocadas no pueden atacar en el mismo turno, pero pueden usar habilidades si están disponibles.
   - Si ha pasado un turno desde que se invocó una carta, esta puede atacar una vez por turno.

3. Ataques:

   - Las cartas solo pueden atacar a las cartas enemigas, no al líder, a menos que no haya cartas enemigas invocadas.
   - Al destruir una carta enemiga, la diferencia entre el ataque y los puntos de vida de la carta se resta de los puntos de vida del jugador contrario.
   - Si no hay cartas enemigas invocadas, se puede atacar directamente al líder enemigo para reducir sus puntos de vida.
   - Las habilidades solo pueden usarse si la carta no ha atacado en el mismo turno.

4. Habilidades del Líder:

   - El jugador puede usar la habilidad especial de su líder en cualquier momento del turno.
   - Estas habilidades son costosas y tienen un cooldown.

5. Fin del Turno:

   - Una vez que el jugador haya invocado sus cartas y no tenga más acciones por realizar, termina su turno.
   - Existe un botón para terminar el turno en cualquier momento, incluso si el jugador aún puede realizar acciones.

6. Fin del Juego:
   - El juego termina cuando los puntos de vida de uno de los jugadores llegan a 0, o si un jugador se rinde o desconecta de la partida.

## _Desarrollo_

---

### **Clases Abstractas / Componentes**

1. BaseCard
2. BasePlayer
3. BaseDeck
4. BaseGame
5. BaseEffect
6. BaseObject
7. BaseInteractable

### **Clases Derivadas / Composición de Componentes**

1. BaseCard
   - Card
   - NationLeader
2. BasePlayer
   - Player
   - AIPlayer
3. BaseDeck
   - Deck
   - DeckManager
4. Effect
   - LeaderEffect
   - CardEffect
5. BaseGame
   - Game
   - Arena
   - Board
   - TurnManager
   - CardManager
6. BaseObject
   - GoldCoin

## _Gráficos_

---

### **Atributos de Estilo**

Nuestro juego tendrá un estilo realista y detallado similar a Age of Empires 2. Esto incluye arquitectura, paisajes y muchos personajes históricos. Vamos a usar bordes sólidos y definidos para destacar diferentes elementos. Usaremos efectos como cambios de color y sombras para darle feedback al jugador sobre sus acciones.

### **Gráficos Necesarios**

1. Personajes con apariencia humana, además de bestias divinas o mitológicas de un país.
2. Carta y plantilla con estados (muerta, activa, en espera)
3. Elementos como fuego, agua, etc (al usar un efecto especial)
4. Animación de espadas (para cartas)
5. Vida (disminuyendo o aumentando)
6. Fondos de cada arena
7. Tablero de cada arena
8. Pantallas de carga y fotos de las arenas
9. Imágenes de cada líder
10. Foto del menú principal

#### Cartas

![Gladiador](imagenes_gdd/Gladiator.png)

![Napoleon](imagenes_gdd/Napoleon.png)

#### Líder de Nación

![Napoleon-icon](imagenes_gdd/icon.png)

#### Menu de Inicio

![menu](imagenes_gdd/menu.png)

#### Arenas

![france](imagenes_gdd/france.png)
![japan](imagenes_gdd/japan.png)
![mexico](imagenes_gdd/mexico.png)
![italy](imagenes_gdd/italy.png)

## _Sonido/Música_

---

### **Atributos de Estilo**

La música refleja la atmósfera épica e histórica del juego. Se usarán instrumentos que crean sonidos antiguos y de guerra como tambores, cuernos, y flautas. Para los efectos sonidos se usarán sonidos realistas como choques de espadas para los ataques, flechas, y caballos galopando. La música tomará algo de influencia de las distintas culturas de las naciones del juego.

### **Sonidos Requeridos**

1. Sonidos

   1. Inicio del juego desde el menú
   2. Click en baraja (menú de selección)
   3. Entrada a la arena de juego
   4. Grito del líder al empezar el juego
   5. Recibir monedas (saco de monedas)
   6. Invocación de carta (grito del líder)
   7. Ataque (sonido de espada)
   8. Carta muere (espadas chocan)
   9. Juego terminado (trompetas)
   10. Música de fondo (strategic song)

2. Música
   1. [https://www.youtube.com/watch?v=ENyxseq59YQ](https://www.youtube.com/watch?v=ENyxseq59YQ)
   2. [https://www.youtube.com/watch?v=PkOC5BnnXwU&list=PL1800E1EFCA1EABE3&index=9](https://www.youtube.com/watch?v=PkOC5BnnXwU&list=PL1800E1EFCA1EABE3&index=9)
   3. [https://www.youtube.com/watch?v=r30D3SW4OVw](https://www.youtube.com/watch?v=r30D3SW4OVw)
   4. [https://www.youtube.com/watch?v=NPX6_qfUIhw](https://www.youtube.com/watch?v=NPX6_qfUIhw)
   5. [https://www.youtube.com/watch?v=IBp0Pu3NeOo&list=PLamnoxId_aK2qxsln0OiDh9s3Pa1vgfvb](https://www.youtube.com/watch?v=IBp0Pu3NeOo&list=PLamnoxId_aK2qxsln0OiDh9s3Pa1vgfvb)
   6. [https://www.youtube.com/watch?v=z0qSFnNxcdc&list=PL5149F2CAD2F42910&index=1](https://www.youtube.com/watch?v=z0qSFnNxcdc&list=PL5149F2CAD2F42910&index=1)
