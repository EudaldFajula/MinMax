using UnityEngine;

public static class MinMax
{
    // ── Punto de entrada ─────────────────────────────────────────────
    public static (int x, int y) GetBestMove(int[,] matrix)
    {
        int bestScore = int.MaxValue; // IA minimiza (es equipo -1)
        int bestX = -1, bestY = -1;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 0)
                {
                    int[,] newMatrix = CopyMatrix(matrix);
                    newMatrix[i, j] = -1; // IA juega

                    int score = MinMaxAlgorithm(newMatrix, int.MinValue, int.MaxValue, false);

                    if (score < bestScore) // IA busca el score más bajo
                    {
                        bestScore = score;
                        bestX = i;
                        bestY = j;
                    }
                }
            }
        }

        Debug.Log($"IA juega en ({bestX},{bestY}) con score {bestScore}");
        return (bestX, bestY);
    }

    // ── Algoritmo MinMax con Alpha-Beta ───────────────────────────────
    // isMaximizing = true  → turno del jugador humano (1)
    // isMaximizing = false → turno de la IA (-1)
    private static int MinMaxAlgorithm(int[,] matrix, int alpha, int beta, bool isMaximizing)
    {
        int result = Calculs.EvaluateWin(matrix);

        if (result == 1) return 10;   // Gana jugador humano
        if (result == -1) return -10;  // Gana IA
        if (result == 0) return 0;    // Empate

        if (isMaximizing) // Turno del jugador humano
        {
            int best = int.MinValue;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        int[,] newMatrix = CopyMatrix(matrix);
                        newMatrix[i, j] = 1; // Jugador humano

                        int score = MinMaxAlgorithm(newMatrix, alpha, beta, false);
                        best = Mathf.Max(best, score);
                        alpha = Mathf.Max(alpha, best);

                        if (beta <= alpha) return best; // Poda
                    }
                }
            }
            return best;
        }
        else // Turno de la IA
        {
            int best = int.MaxValue;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        int[,] newMatrix = CopyMatrix(matrix);
                        newMatrix[i, j] = -1; // IA

                        int score = MinMaxAlgorithm(newMatrix, alpha, beta, true);
                        best = Mathf.Min(best, score);
                        beta = Mathf.Min(beta, best);

                        if (beta <= alpha) return best; // Poda
                    }
                }
            }
            return best;
        }
    }

    // ── Copia la matriz ───────────────────────────────────────────────
    private static int[,] CopyMatrix(int[,] original)
    {
        int rows = original.GetLength(0);
        int cols = original.GetLength(1);
        int[,] copy = new int[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                copy[i, j] = original[i, j];
        return copy;
    }
}