import React from "react";

const Book = ({ book, openEditBook, openDeleteBook }) => {
    const {id, title, author, genre, dateCreating, count } = book;

    return(
        <tr>
            <td>{title}</td>
            <td>{author}</td>
            <td>{genre.title}</td>
            <td>{dateCreating.substr(0,4)}</td>
            <td>{count}</td>
            <td>
                <button className="btn btn-warning mr-3" onClick={() => openEditBook(true,id)}>Изменить</button>
                <button className="btn btn-danger" onClick={() => openDeleteBook(true,id)}>Удалить</button>
            </td>
        </tr>
    );
};

export default Book;