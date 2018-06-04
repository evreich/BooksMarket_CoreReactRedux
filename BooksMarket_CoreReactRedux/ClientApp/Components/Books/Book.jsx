import React from "react";

const Book = ({ book, openEditBook, openDeleteBook }) => {
    const {id, title, author, genre, date, count } = book;

    return(
        <tr>
            <td>{title}</td>
            <td>{author}</td>
            <td>{genre}</td>
            <td>{date}</td>
            <td>{count}</td>
            <td>
                <button className="btn btn-info" onClick={() => openEditBook(true,id)}>Изменить</button>
                <button className="btn btn-info" onClick={() => openDeleteBook(true,id)}>Удалить</button>
            </td>
        </tr>
    );
};

export default Book;