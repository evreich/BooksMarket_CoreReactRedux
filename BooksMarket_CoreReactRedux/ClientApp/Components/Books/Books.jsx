import React from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";

import { getBooksAction } from "../../Actions/BooksActions";
import { clearRequestResult } from "../../Actions/CommonActions";
import AddBook from "./AddBook";
import EditBook from "./EditBook";
import DeleteBook from "./DeleteBook";
import Book from "./Book";
import "./Books.css";

class Books extends React.PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            showAddBookWindow: false,
            showEditBookWindow: false,
            showDeleteBookWindow: false,
            choosedBookId: 0
        };
    }

    static propTypes = {
        getBooks: PropTypes.func,
        clearRequestResult: PropTypes.func,
        books: PropTypes.array
    };

    showAddBookSetState = (state, bookId) => {
        this.setState({
            showAddBookWindow: state,
            choosedBookId: bookId
        });
    };

    showEditBookSetState = (state, bookId) => {
        this.setState({
            showEditBookWindow: state,
            choosedBookId: bookId
        });
    };

    showDeleteBookSetState = (state, bookId) => {
        this.setState({
            showDeleteBookWindow: state,
            choosedBookId: bookId
        });
    };

    componentDidMount = () => {
        this.props.getBooks("", 1, this.props.token);
    };

    componentWillUnmount = () => {
        this.props.clearRequestResult();
    };

    handleAddBookClick = event => {
        event.target;
    };

    render() {
        const { books, actionResult } = this.props;

        const requestResultMessage = actionResult ? (
            <div className="alert alert-info" role="alert">
                {actionResult}
            </div>
        ) : null;

        const {
            showAddBookWindow,
            showEditBookWindow,
            showDeleteBookWindow,
            choosedBookId
        } = this.state;

        const showAddBookComponent = showAddBookWindow ? (
            <AddBook book={books.find(book => book.id === choosedBookId)} />
        ) : null;
        const showEditBookComponent = showEditBookWindow ? (
            <EditBook book={books.find(book => book.id === choosedBookId)} />
        ) : null;
        const showDeleteBookComponent = showDeleteBookWindow ? (
            <DeleteBook book={books.find(book => book.id === choosedBookId)} />
        ) : null;

        return (
            <div className="container mt-5">
                {/* Выпадающие окна на CRUD действия над книгами */}
                {showAddBookComponent}
                {showEditBookComponent}
                {showDeleteBookComponent}
                <h1>Список книг</h1>
                <br />
                {requestResultMessage}
                <table className="table-condensed">
                    <tr>
                        <th className="text-center">Название</th>
                        <th className="text-center">Автор</th>
                        <th className="text-center">Жанр</th>
                        <th className="text-center">Дата создания</th>
                        <th className="text-center">Количество</th>
                        <th className="text-center">Действия</th>
                    </tr>
                    {books.map(book => (
                        <Book
                            key={`${book.id}_${book.title}`}
                            book={book}
                            openEditBook={this.showEditBookSetState}
                            openDeleteBook={this.showDeleteBookSetState}
                        />
                    ))}
                </table>
            </div>
        );
    }
}

const mapStateOnProps = state => ({
    books: state.books,
    actionResult: state.requestResult
});

const mapDispatchOnProps = dispatch => ({
    getBooks(searchExpr, page, token) {
        dispatch(getBooksAction(searchExpr, page, token));
    },
    clearRequestResult() {
        dispatch(clearRequestResult());
    }
});

export default connect(mapStateOnProps, mapDispatchOnProps)(Books);
