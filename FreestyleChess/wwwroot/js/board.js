// Aktuell ausgewähltes Piece merken
let selectedPiece = null;

function clearSelection() {
    document.querySelectorAll('.square.selected')
        .forEach(sq => sq.classList.remove('selected'));
}

function clearMarkedSquares() {
    document.querySelectorAll('.square.marked').forEach(sq => sq.classList.remove('marked'));
}


function attachSquareListeners() {
    const squares = document.querySelectorAll('.square');

    squares.forEach(square => {
        square.addEventListener('click', (e) => {

            if (e.button === 0) {
                clearMarkedSquares();
            }


            const rank = parseInt(square.dataset.rank);
            const file = parseInt(square.dataset.file);

            const clickedPieceEl = square.querySelector('.piece');

            if(selectedPiece) {
                if (selectedPiece.rank === rank && selectedPiece.file === file) {
                    // Gleiche Square angeklickt, Auswahl aufheben
                    clearSelection();
                    selectedPiece = null;
                }
                else if(clickedPieceEl)
                {
                    const selectedPieceColor = document.querySelector(`.square[data-rank='${selectedPiece.rank}'][data-file='${selectedPiece.file}'] .piece`).dataset.color;
                    const clickedPieceColor = clickedPieceEl.dataset.color;

                    if (selectedPieceColor === clickedPieceColor)
                    {
                        clearSelection();
                        selectedPiece = { rank, file };
                        square.classList.add("selected");
                        return;
                    }
                    else
                    {
                        clearSelection();
                        movePiece(selectedPiece.rank, selectedPiece.file, rank, file);
                        selectedPiece = null;
                    }
                }
                else
                {
                    clearSelection();
                    movePiece(selectedPiece.rank, selectedPiece.file, rank, file);
                    selectedPiece = null;
                }
               
            } 
            else if(square.querySelector('.piece')) {
                clearSelection();
                selectedPiece = { rank, file };
                square.classList.add("selected");
            }
        });

        square.addEventListener("contextmenu", (e) => {
            e.preventDefault(); // Standard-Kontextmenü verhindern

            if (selectedPiece) {
                const selSquare = document.querySelector(`.square[data-rank='${selectedPiece.rank}'][data-file='${selectedPiece.file}']`);
                if (selSquare) selSquare.classList.remove("selected");
                selectedPiece = null;
            }
            else
            {
                square.classList.toggle("marked");
            }
            
        });

    });
}




// Funktion zum Bewegen eines Pieces
function movePiece(fromRank, fromFile, toRank, toFile)
{
    fetch('/Game/Move',
        {
            method: 'POST', // POST, weil wir Daten senden
            headers: { 'Content-Type': 'application/json' }, // Wir senden JSON
            body: JSON.stringify(
                {    
                fromRank, 
                fromFile, 
                toRank, 
                toFile 
                })
        })
    .then(response => 
        {
            if (!response.ok) 
            {
                alert("Move ungültig");
                throw new Error("Move failed");
            }
            return response.json(); // Antwort vom Server als JSON parsen
        })
    .then(boardData => 
        {   
            renderBoard(boardData); // Neues Board darstellen
        })
    .catch(error => console.error(error));
}






function renderBoard(boardData) {
    // Alle Squares im DOM auswählen
    const squares = document.querySelectorAll('.square');

    squares.forEach(square => {
        const rank = parseInt(square.dataset.rank);
        const file = parseInt(square.dataset.file);

        // Vorherige Figur entfernen
        while (square.firstChild) {
            square.removeChild(square.firstChild);
        }

        // Neue Figur aus boardData einfügen
        const squareInfo = boardData.squares.find(sq => sq.rank === rank && sq.file === file);

        if(squareInfo.piece) {
            const img = document.createElement('img');
            img.classList.add('piece');
            img.src = `/images/pieces/${squareInfo.piece.color.toLowerCase()}_${squareInfo.piece.type.toLowerCase()}.svg`;
            img.alt = squareInfo.piece.type;
            img.dataset.color = squareInfo.piece.color.toLowerCase(); 
            square.appendChild(img);
        }
    });

}


document.addEventListener('DOMContentLoaded', () => {
    attachSquareListeners();
});