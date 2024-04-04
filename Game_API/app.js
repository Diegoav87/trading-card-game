"use strict";

import { check, validationResult } from "express-validator";
import express from "express";
import mysql from "mysql2/promise"

const app = express();

const port = 5000;

app.use(express.json());

async function connectToDB() {
  return await mysql.createConnection({
    host: "localhost",
    user: "root",
    password: "diego",
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

app.post("/api/players", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const data = request.body;
    console.log(data.name)

    await check(data.name).custom((value) => {
      if (!value || value === '') {

      }
      return true;
    });

    const errors = validationResult(request);
    if (!errors.isEmpty()) {
      return response.status(400).json({ errors: errors.array() });
    }

    const playerData = request.body;

    const [results, fields] = await connection.execute(
      "INSERT INTO player (name) VALUES (?)",
      [playerData.name]
    );

    console.log(`${results.affectedRows} rows affected`);

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

app.get("/api/players/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute("SELECT * FROM player WHERE player_id = ?", [request.params.id]);

    if (results.length === 0) {
      return response.status(404).json({ error: "No se encontró ningun jugador con el ID proporcionado" });
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


app.get("/api/decks/:id", async (request, response) => {
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

app.post("/api/games", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const data = request.body;

    console.log("Duration value:", data.duration);

    await Promise.all([
      check(data.player1_id).isInt({ min: 1 }).withMessage("El ID del jugador 1 debe ser un número entero positivo"),
      check(data.player2_id).isInt({ min: 1 }).withMessage("El ID del jugador 2 debe ser un número entero positivo"),
      check(data.winner_id).isInt({ min: 1 }).withMessage("El ID del ganador debe ser un número entero positivo"),
      check(data.arena_id).isInt({ min: 1 }).withMessage("El ID de la arena debe ser un número entero positivo"),
      check(data.duration).isInt({ min: 1, max: 3600 }).withMessage("La duración del juego debe ser un número entero positivo menor o igual a 3600 segundos")
    ]).then(() => {
      const errors = validationResult(request);
      if (!errors.isEmpty()) {
        throw errors.array();
      }
    });



    // Verificar la existencia de los jugadores y la arena en la base de datos
    const [player1Result] = await connection.execute("SELECT 1 FROM player WHERE player_id = ?", [data.player1_id]);
    if (player1Result.length === 0) {
      return response.status(400).json({ error: `El jugador con ID ${data.player1_id} no existe en la base de datos` });
    }

    const [player2Result] = await connection.execute("SELECT 1 FROM player WHERE player_id = ?", [data.player2_id]);
    if (player2Result.length === 0) {
      return response.status(400).json({ error: `El jugador con ID ${data.player2_id} no existe en la base de datos` });
    }

    const [winnerResult] = await connection.execute("SELECT 1 FROM player WHERE player_id = ?", [data.winner_id]);
    if (winnerResult.length === 0) {
      return response.status(400).json({ error: `El jugador con ID ${data.winner_id} no existe en la base de datos` });
    }

    const [arenaResult] = await connection.execute("SELECT 1 FROM arena WHERE arena_id = ?", [data.arena_id]);
    if (arenaResult.length === 0) {
      return response.status(400).json({ error: `La arena con ID ${data.arena_id} no existe en la base de datos` });
    }

    const [results, fields] = await connection.execute(
      "INSERT INTO game (player1_id, player2_id, winner_id, arena_id, duration) VALUES (?,?,?,?,?)",
      [data.player1_id, data.player2_id, data.winner_id, data.arena_id, data.duration]
    );
    console.log(`${results.affectedRows} Game affected`);
    console.log(results);

    response.status(200).json({ message: "Game added successfully" });
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
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

    const [results, fields] = await connection.execute("SELECT * FROM deck");

    response.status(200).json(results);
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