const path = require("path");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const bundleFolder = "./wwwroot/dist/";
const srcFolder = "./ClientApp/";

module.exports = {
    entry: {
        "main": [`${srcFolder}index.jsx`, "isomorphic-fetch", "babel-polyfill"]
    },
    resolve: {
        extensions: [".js", ".jsx"]
    },
    output: {
        filename: "bundle.js",
        path: path.resolve(__dirname, bundleFolder),
        publicPath: "dist/",
    },
    devtool: "inline-source-map",
    module: {
        rules: [
            {   
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader",
                    options: {
                        plugins: [
                            "transform-runtime", 
                            "transform-async-generator-functions", 
                            "transform-object-rest-spread",
                            "transform-class-properties",
                            "react-hot-loader/babel"
                        ],
                        presets: ["env", "stage-3", "react"]
                    }
                }
            },
            {
                test: /\.css$/,
                use: ExtractTextPlugin.extract({
                    fallback: "style-loader",
                    use: "css-loader"
                })
            },
            {
                test: /\.scss$/,
                use: ExtractTextPlugin.extract({
                    fallback: "style-loader",
                    use: ["css-loader", "sass-loader"]
                })
            }
        ]
    },
    plugins: [
        new ExtractTextPlugin("styles.css"),
    ]
};