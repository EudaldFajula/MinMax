### Algoritmo MinMax
La IA utiliza el algoritmo **MinMax** para evaluar todos los estados futuros posibles del tablero y elegir la mejor jugada. Funciona así:

- **Maximiza** su propia puntuación (turno de la IA)
- **Minimiza** la puntuación del jugador (turno del jugador)
- Explora recursivamente el árbol de juego completo hasta llegar a un estado terminal (victoria, derrota o empate)

### Poda Alpha-Beta
Para mejorar el rendimiento, el algoritmo usa **Poda Alpha-Beta**, que descarta ramas del árbol de juego que no pueden afectar al resultado final, reduciendo cálculos innecesarios sin alterar la decisión.

### Evaluación del Tablero
El tablero se representa como una **matriz de enteros 3×3**:
- `1` → Jugador
- `-1` → IA
- `0` → Celda vacía

Las condiciones de victoria se comprueban en filas, columnas y diagonales. La función `EvaluateWin()` devuelve:
- `1` → Gana el jugador
- `-1` → Gana la IA
- `0` → Empate
- `2` → Partida en curso

---

### Scripts Principales

| Script | Responsabilidad |
|---|---|
| `GameManager.cs` | Gestiona los estados del juego (`CanMove` / `CantMove`), input del jugador, instancia las fichas |
| `MinMax.cs` | Implementa el algoritmo MinMax con Poda Alpha-Beta y devuelve la mejor jugada |
| `Calculs.cs` | Evalúa condiciones de victoria, mapea clics de pantalla a coordenadas del tablero |
| `Node.cs` | Representa un nodo en el árbol de decisión MinMax (posición, puntuación, alpha, beta) |
