"use strict";

import { check, body, validationResult } from "express-validator";
import express, { response } from "express";
import mysql from "mysql2/promise";
import cors from 'cors';
import { fileURLToPath } from 'url';
import { dirname, join } from 'path';

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

const app = express();

const port = 5000;
app.use(cors());
app.use(express.json());
app.use(express.static(join(__dirname, 'public')));

async function connectToDB() {
  return await mysql.createConnection({
    host: "localhost",
    user: "Gomesinho",
    password: "andres",
    database: "time_clash_chronicles",
  });
}

app.get("/api/cards", async (request, response) => {
  let connection = null;

  try {

    connection = await connectToDB();

    const [results, fields] = await connection.execute("select * from card");

    response.status(200).json(results);
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {

    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

app.get("/api/cards/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute("SELECT * FROM card WHERE card_id = ?", [request.params.id]);

    if (results.length === 0) {
      return response.status(404).json({ error: "No se encontró ninguna carta con el ID proporcionado" });
    }

    response.status(200).json(results[0]);
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {

    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

app.post("/api/players", [
  body('username').notEmpty().withMessage('Username cannot be empty'),
  body('password').notEmpty().withMessage("Password can't be empty")
], async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const errors = validationResult(request);
    if (!errors.isEmpty()) {
      return response.status(400).json({ errors: errors.array() });
    }

    const playerData = request.body;

    const [results, fields] = await connection.execute(
      "INSERT INTO player (username, password) VALUES (?, ?)",
      [playerData.username, playerData.password]
    );

    response.status(200).json({ message: "Jugador creado exitosamente" });
  } catch (error) {
    console.error(error);
    response.status(500).json({ error: "Error al crear el jugador" });
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Conexión cerrada exitosamente");
    }
  }
});

app.post("/api/login", [
  body('username').notEmpty().withMessage('Username cannot be empty'),
  body('password').notEmpty().withMessage("Password can't be empty")
], async (request, response) => {

  let connection = null;

  try {
    connection = await connectToDB();

    const errors = validationResult(request);
    if (!errors.isEmpty()) {
      return response.status(400).json({ errors: errors.array() });
    }

    const { username, password } = request.body;

    const [results, fields] = await connection.execute('SELECT * FROM player WHERE username = ?', [username]);

    if (results.length === 0) {
      return response.status(401).json({ error: "Nombre de usuario inválido" });
    }

    const player = results[0];

    if (password != player.password) {
      return response.status(401).send('Contraseña inválida');
    }


    response.status(200).json(player);
  } catch (error) {
    console.error('Error logging in:', error);
    response.status(500).send('Error logging in');
  }
});


app.get("/api/players/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute("SELECT * FROM player WHERE player_id = ?", [request.params.id]);

    if (results.length === 0) {
      return response.status(404).json({ error: "No se encontró ningun jugador con el ID proporcionado" });
    }

    response.status(200).json(results[0]);
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {

    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});


app.get("/api/decks/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute(`
      SELECT d.deck_id, d.name AS deck_name, d.description, d.nation,
             c.card_id, c.name AS card_name, c.attack, c.health, c.basic_attack, c.cost, c.nation AS card_nation,
             l.leader_id, l.name AS leader_name,
             a.ability_id, a.name AS ability_name, a.description AS ability_description, a.cost AS ability_cost, a.type AS ability_type
      FROM deck d
      LEFT JOIN card c ON d.deck_id = c.deck_id
      LEFT JOIN leader l ON d.deck_id = l.deck_id
      LEFT JOIN ability a ON c.ability_id = a.ability_id
      WHERE d.deck_id = ?
    `, [request.params.id]);

    if (results.length === 0) {
      return response.status(404).json({ error: "No se encontró ningun deck con el ID proporcionado" });
    }

    const deck = {
      deck_id: results[0].deck_id,
      name: results[0].deck_name,
      description: results[0].description,
      nation: results[0].nation,
      leader: {
        leader_id: results[0].leader_id,
        name: results[0].leader_name,
      },
      cards: results.map(row => ({
        card_id: row.card_id,
        name: row.card_name,
        attack: row.attack,
        health: row.health,
        basic_attack: row.basic_attack,
        cost: row.cost,
        nation: row.card_nation,
        ability: row.ability_id ? { // Check if the card has an ability
          ability_id: row.ability_id,
          name: row.ability_name,
          description: row.ability_description,
          cost: row.ability_cost,
          type: row.ability_type
        } : null
      })).filter(card => card.card_id)
    };

    response.status(200).json(deck);
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

app.get("/api/player/winrate", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();
    const [results, fields] = await connection.execute("SELECT SUM(win) AS total_wins, COUNT(*) - SUM(win) AS total_losses, CASE WHEN COUNT(*) = 0 THEN 0 ELSE (SUM(win) / COUNT(*)) * 100 END AS total_win_rate FROM game;");

    if (results.length === 0) {
      return response.status(404).json({ error: "No existe información suficiente" });
    }


    const winrate = parseFloat(results[0].total_win_rate).toFixed(2); // Convert winrate to float with 2 decimal places
    response.status(200).json({ winrate }); // Return winrate as an object with a key "winrate"
  } catch (error) {
    response.status(500).json({ error: error.message });
    console.log(error);
  }
});

app.get("/api/game/bestdecks", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();
    const [results, fields] = await connection.execute("SELECT deck.name, SUM(game.win) AS victorias_totales, COUNT(game.win) AS partidas_totales, (SUM(game.win) / COUNT(game.win)) * 100 AS porcentaje_victoria FROM game INNER JOIN deck ON game.deck_id = deck.deck_id GROUP BY game.deck_id ORDER BY porcentaje_victoria DESC, game.deck_id DESC LIMIT 5;");

    if (results.length === 0) {
      return response.status(404).json({ error: "Data not found" });
    }

    const mappedResults = results.map(result => ({
      name: result.name,
      victorias_totales: parseInt(result.victorias_totales), // Convert to integer
      partidas_totales: parseInt(result.partidas_totales), // Convert to integer
      porcentaje_victoria: parseFloat(result.porcentaje_victoria).toFixed(2) // Convert to float with 2 decimal places
    }));
    response.status(200).json(mappedResults);
  } catch (error) {
    response.status(500).json({ error: error.message });

    console.log(error);
  }
});

app.get("/api/game/mostusedecks", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();
    const [results, fields] = await connection.execute("SELECT name AS Deck, COUNT(game.deck_id) AS top_deck FROM game INNER JOIN deck ON game.deck_id = deck.deck_id GROUP BY game.deck_id ORDER BY top_deck DESC, game.deck_id DESC LIMIT 5;");

    if (results.length === 0) {
      return response.status(404).json({ error: "Deck not found" });
    }
    const mappedResults = results.map(result => ({
      Deck: result.Deck,
      top_deck: parseInt(result.top_deck) // Convert to integer
    }));
    response.status(200).json(mappedResults);
  } catch (error) {
    response.status(500).json({ error: error.message });
    console.log(error);
  }
});

app.post("/api/games", [
  body('player_id').isInt({ min: 1 }).withMessage("El ID del jugador debe ser un número entero positivo"),
  body('deck_id').isInt({ min: 1 }).withMessage("El ID del deck debe ser un número entero positivo"),
  body('win').isBoolean().withMessage("El win debe ser un booleano"),
], async (request, response) => {
  let connection = null;

  try {
    const errors = validationResult(request);
    if (!errors.isEmpty()) {
      return response.status(400).json({ errors: errors.array() });
    }

    const data = request.body;
    connection = await connectToDB();

    const [playerResult] = await connection.execute("SELECT 1 FROM player WHERE player_id = ?", [data.player_id]);
    if (playerResult.length === 0) {
      return response.status(400).json({ error: `El jugador con ID ${data.player1_id} no existe en la base de datos` });
    }

    const [results, fields] = await connection.execute(
      "INSERT INTO game (player_id, deck_id, win) VALUES (?,?,?)",
      [data.player_id, data.deck_id, data.win]
    );

    console.log(`${results.affectedRows} Game affected`);
    console.log(results);

    response.status(200).json({ message: "Game added successfully" });
  }
  catch (error) {
    console.error("Error creating game:", error);
    response.status(500).json({ error: "Error creating game" });
  }
  finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});


app.get("/api/games/:gameId", async (request, response) => {
  const gameId = request.params.gameId;
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute(
      "SELECT * FROM game WHERE game_id = ?",
      [gameId]
    );

    if (results.length === 0) {
      return response.status(404).json({ error: "No se encontró ningun juego con el ID proporcionado" });
    }

    if (results.length === 0) {
      response.status(404).json({ error: "Game not found" });
    } else {
      response.status(200).json(results[0]);
    }
  } catch (error) {
    response.status(500).json({ error: "Internal Server Error" });
    console.error(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});



app.get("/api/decks", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute(`
      SELECT d.*, l.leader_id, l.name AS leader_name
      FROM deck d
      LEFT JOIN leader l ON d.deck_id = l.deck_id
    `);

    const decks = results.map(row => ({
      deck_id: row.deck_id,
      name: row.name,
      description: row.description,
      nation: row.nation,
      leader: row.leader_id ? {
        leader_id: row.leader_id,
        name: row.leader_name,
      } : null
    }));

    response.status(200).json(decks);
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

app.get("/api/deck/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute("SELECT * FROM deck WHERE deck_id = ?", [request.params.id]);

    if (results.length === 0) {
      return response.status(404).json({ error: "No se encontró ningun deck con el ID proporcionado" });
    }

    response.status(200).json(results);
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {

    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

app.get("/api/decks/:deckId/cards", async (request, response) => {
  const deckId = request.params.deckId;
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute(
      `SELECT * from card where deck_id = ?`,
      [deckId]
    );

    if (results.length === 0) {
      return response.status(404).json({ error: "No se encontró ningun deck con el ID proporcionado" });
    }

    response.status(200).json(results);
  } catch (error) {
    response.status(500).json({ error: "Internal Server Error" });
    console.error(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

app.listen(port, () => {
  console.log(`Escuchando en el puerto ${port}`);
});