using UnityEngine;

public static class MinMax
{
    // ── Punto de entrada ─────────────────────────────────────────────
    public static (int x, int y) GetBestMove(int[,] matrix)
    {
        Node root = new Node(
            parent: null,
            team: 1,
            alpha: int.MinValue,
            beta: int.MaxValue,
            x: -1,
            y: -1,
            matrixNode: CopyMatrix(matrix)
        );

        MinMaxAlgorithm(root, false); // false = turno IA primero

        if (root.BestChild == null)
        {
            Debug.LogError("MinMax no encontró jugada!");
            return (-1, -1);
        }

        Debug.Log($"IA juega en ({root.BestChild.X},{root.BestChild.Y}) score={root.BestChild.Value}");
        return (root.BestChild.X, root.BestChild.Y);
    }

    // ── Algoritmo MinMax con Alpha-Beta usando Node ───────────────────
    private static int MinMaxAlgorithm(Node node, bool isMaximizing)
    {
        int result = Calculs.EvaluateWin(node.MatrixNode);

        if (result == 1) return 10;
        if (result == -1) return -10;
        if (result == 0) return 0;

        int alpha = node.Alpha;
        int beta = node.Beta;

        if (isMaximizing) // Turno jugador humano (1) — maximiza
        {
            int best = int.MinValue;

            for (int i = 0; i < node.MatrixNode.GetLength(0); i++)
            {
                for (int j = 0; j < node.MatrixNode.GetLength(1); j++)
                {
                    if (node.MatrixNode[i, j] == 0)
                    {
                        Node child = new Node(
                            parent: node,
                            team: 1,
                            alpha: alpha,
                            beta: beta,
                            x: i,
                            y: j,
                            matrixNode: CopyMatrix(node.MatrixNode)
                        );
                        node.NodeChildren.Push(child);

                        int score = MinMaxAlgorithm(child, false);
                        child.Value = score;

                        if (score > best)
                        {
                            best = score;
                            node.BestChild = child;
                        }

                        alpha = Mathf.Max(alpha, best);
                        node.Alpha = alpha;

                        if (beta <= alpha)
                        {
                            child.Pruned = true;
                            return best;
                        }
                    }
                }
            }
            return best;
        }
        else // Turno IA (-1) — minimiza
        {
            int best = int.MaxValue;

            for (int i = 0; i < node.MatrixNode.GetLength(0); i++)
            {
                for (int j = 0; j < node.MatrixNode.GetLength(1); j++)
                {
                    if (node.MatrixNode[i, j] == 0)
                    {
                        Node child = new Node(
                            parent: node,
                            team: -1,
                            alpha: alpha,
                            beta: beta,
                            x: i,
                            y: j,
                            matrixNode: CopyMatrix(node.MatrixNode)
                        );
                        node.NodeChildren.Push(child);

                        int score = MinMaxAlgorithm(child, true);
                        child.Value = score;

                        if (score < best)
                        {
                            best = score;
                            node.BestChild = child;
                        }

                        beta = Mathf.Min(beta, best);
                        node.Beta = beta;

                        if (beta <= alpha)
                        {
                            child.Pruned = true;
                            return best;
                        }
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