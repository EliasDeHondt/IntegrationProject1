﻿const path = require('path');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    entry: {
        site: './src/js/site.ts',
        validation: './src/js/validation.ts',
        index: './src/js/index.ts',
        step: './src/js/Flow/StepAPI.ts',
        theme: './src/js/Theme/ThemeAPI.ts',
    },
    output: {
        filename: '[name].entry.js',
        path: path.resolve(__dirname, '..', 'wwwroot', 'dist'),
        clean: true
    },
    devtool: 'source-map',
    mode: 'development',
    resolve: {
        extensions: [".ts", ".js"],
        extensionAlias: {
            '.js': ['.js', '.ts'],
        },
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/i,
                use: 'ts-loader',
                exclude: /node_modules/
            },
            {
                test: /\.s?css$/i,
                use: [
                    MiniCssExtractPlugin.loader,
                    "css-loader",
                    "sass-loader"
                ]
            },
            {
                test: /\.(png|svg|jpg|jpeg|gif|webp)$/i,
                type: "asset/resource"
            },
            {
                test: /\.(eot|woff(2)?|ttf|otf|svg)$/i,
                type: 'asset/resource'
            },
            {
                test: /\.(mp3|wav|ogg|mp4|webm|mkv)$/i,
                type: 'asset/resource'
            },
        ]
    },
    plugins: [
        new MiniCssExtractPlugin(),
    ]
};
