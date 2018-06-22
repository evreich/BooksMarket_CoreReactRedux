import React from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";

import { getAllBooksAction } from "../../Actions/BooksActions";
import { clearRequestResult } from "../../Actions/CommonActions";
import AddBook from "./AddBook";
import EditBook from "./EditBook";
import DeleteBook from "./DeleteBook";
import Book from "./Book";
import "./Books.css";
import Loading from "../Layouts/Loading";

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

    showAddBookSetState = (isNeedShow) => {
        this.setState({
            showAddBookWindow: isNeedShow
        });
    };

    showEditBookSetState = (isNeedShow, bookId) => {
        this.setState({
            showEditBookWindow: isNeedShow,
            choosedBookId: bookId
        });
    };

    showDeleteBookSetState = (isNeedShow, bookId) => {
        this.setState({
            showDeleteBookWindow: isNeedShow,
            choosedBookId: bookId
        });
    };

    componentDidMount = () => {
        this.props.getBooks();
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
            <div id="alert" className="alert alert-info" role="alert">
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
            <AddBook showAddBookSetState={this.showAddBookSetState} />
        ) : null;
        const showEditBookComponent = showEditBookWindow ? (
            <EditBook book={books.find(book => book.id === choosedBookId)} showEditBookSetState={this.showEditBookSetState} />
        ) : null;
        const showDeleteBookComponent = showDeleteBookWindow ? (
            <DeleteBook book={books.find(book => book.id === choosedBookId)} showDeleteBookSetState={this.showDeleteBookSetState}/>
        ) : null;

        return (
            !books.length ? <Loading /> :
            <div className="container">
                {/* Выпадающие окна на CRUD действия над книгами */}
                {showAddBookComponent}
                {showEditBookComponent}
                {showDeleteBookComponent}
                <h1>Список книг</h1>
                <br />
                {requestResultMessage}
                <table className="booksTable">
                    <thead>
                        <tr>
                            <th className="text-center">Название</th>
                            <th className="text-center">Автор</th>
                            <th className="text-center">Жанр</th>
                            <th className="text-center">Дата создания</th>
                            <th className="text-center">Количество</th>
                            <th className="text-center">Действия</th>
                        </tr>
                    </thead>
                    <tbody>
                        {books.map(book => (
                            <Book
                                key={`${book.id}_${book.title}`}
                                book={book}
                                openEditBook={this.showEditBookSetState}
                                openDeleteBook={this.showDeleteBookSetState}
                            />
                        ))}
                    </tbody>
                </table>
                <br />
                <button className="btn btn-success" onClick={() => this.showAddBookSetState(true)}>Добавить книгу</button>
            </div>
        );
    }
}

const mapStateOnProps = state => ({
    books: state.books,
    actionResult: state.requestResult
});

const mapDispatchOnProps = dispatch => ({
    getBooks() {
        dispatch(getAllBooksAction());
    },
    clearRequestResult() {
        dispatch(clearRequestResult());
    }
});

export default connect(mapStateOnProps, mapDispatchOnProps)(Books);
