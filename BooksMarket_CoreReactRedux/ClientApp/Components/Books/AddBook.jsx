import React from "react";
import { connect } from "react-redux";
import { addBookAction } from "../../Actions/BooksActions";
import { getGenresAction } from "../../Actions/GenresActions";
import { checkFieldsIsFilled } from "../../Utils/Validators";
import { clearRequestResult } from "../../Actions/CommonActions";

import PropTypes from "prop-types";
import "../../../node_modules/bootstrap-datepicker/dist/js/bootstrap-datepicker";
import "../../../node_modules/bootstrap-datepicker/dist/css/bootstrap-datepicker.css";
import $ from "../../../node_modules/jquery/dist/jquery";
import "./Books.css";

class AddBook extends React.PureComponent {
    constructor(props) {
        super(props);

        this.titleInputRef = React.createRef();
        this.authorInputRef = React.createRef();
        this.dateInputRef = React.createRef();
        this.countInputRef = React.createRef();
        this.genreIdInputRef = React.createRef();

        this.state = {
            titleError: "",
            authorError: "",
            dateError: "",
            countError: "",
            genreError: ""
        };
    }

    static propTypes = {
        onAddBook: PropTypes.func,
        getGenres: PropTypes.func,
        clearRequestResult: PropTypes.func,
        genres: PropTypes.array,
        token: PropTypes.string,
        addBookResult: PropTypes.string
    };

    componentDidMount = () => {
        this.$dateInputRef = $(this.dateInputRef.current);
        this.$dateInputRef.datepicker({
            format: " yyyy",
            viewMode: "years",
            minViewMode: "years",
            autoclose: true
        });

        const { getGenres, genres, token } = this.props;
        if (genres[0].id === 0) getGenres(token);
    };

    componentWillUnmount = () => {
        this.$dateInputRef.datepicker("destroy");
        this.props.clearRequestResult();
    };

    handleSubmit = event => {
        const { onAddBook, token } = this.props;
        const inputs = [
            this.titleInputRef,
            this.dateInputRef,
            this.authorInputRef,
            this.genreIdInputRef,
            this.countInputRef
        ];
        const errors = [
            "titleError",
            "authorError",
            "dateError",
            "genreError",
            "countError"
        ];

        event.preventDefault();
        if (checkFieldsIsFilled(inputs, errors, this.setState.bind(this)))
            onAddBook(
                {
                    Title: this.titleInputRef.current.value,
                    DateCreating: `01.01.${this.dateInputRef.current.value.trim()}`,
                    Author: this.authorInputRef.current.value,
                    GenreId: this.genreIdInputRef.current.value,
                    Count: this.countInputRef.current.value
                },
                token
            );
    };

    render() {
        const { genres } = this.props;
        const {
            titleError,
            authorError,
            dateError,
            genreError,
            countError
        } = this.state;

        return (
            <div id="myModal" className="modal">
                <div className="modal-content">
                    <div className="modal-header">
                        <span className="close">&times;</span>
                        <h2>Добавление книги</h2>
                    </div>
                    <div className="modal-body">
                        <form method="post" onSubmit={this.handleSubmit}>
                            <div className="form-group">
                                <label className="control-label">
                                    Название:
                                </label>
                                <br />
                                <input
                                    ref={this.titleInputRef}
                                    type="text"
                                    className="form-control"
                                />
                                <span className="text-danger">
                                    {titleError}
                                </span>
                            </div>
                            <div className="form-group">
                                <label className="control-label">Автор:</label>
                                <br />
                                <input
                                    ref={this.authorInputRef}
                                    type="text"
                                    className="form-control"
                                />
                                <span className="text-danger">
                                    {authorError}
                                </span>
                            </div>
                            <div className="form-group">
                                <label className="control-label">Дата:</label>
                                <br />
                                <input
                                    ref={this.dateInputRef}
                                    type="text"
                                    id="datepicker"
                                    className="form-control"
                                />
                                <span className="text-danger">{dateError}</span>
                            </div>
                            <div className="form-group">
                                <label className="control-label">Жанр:</label>
                                <br />
                                <select
                                    className="form-control"
                                    ref={this.genreIdInputRef}
                                >
                                    {genres.map(genre => (
                                        <option
                                            key={`${genre.id}_${genre.title}`}
                                            value={genre.id}
                                        >
                                            {genre.title}
                                        </option>
                                    ))}
                                </select>
                                <span className="text-danger">
                                    {genreError}
                                </span>
                            </div>
                            <div className="form-group">
                                <label className="control-label">
                                    Количество:
                                </label>
                                <br />
                                <input
                                    ref={this.countInputRef}
                                    type="number"
                                    min="1"
                                    max="10"
                                    className="form-control"
                                />
                                <span className="text-danger">
                                    {countError}
                                </span>
                            </div>
                            <input
                                type="submit"
                                className="btn btn-block btn-success"
                                value="Добавить книгу"
                            />
                        </form>
                    </div>
                    <div className="modal-footer">
                        <h3>Modal Footer</h3>
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateOnProps = state => ({
    token: state.user ? state.user.token : undefined,
    genres:
        state.genres.length === 0
            ? [{ id: 0, title: "Список загружается..." }]
            : state.genres
});

const mapDispatchOnProps = dispatch => ({
    onAddBook(bookInfo, token) {
        dispatch(addBookAction(bookInfo, token));
    },
    getGenres(token) {
        dispatch(getGenresAction(token));
    },
    clearRequestResult() {
        dispatch(clearRequestResult());
    }
});

export default connect(mapStateOnProps, mapDispatchOnProps)(AddBook);
