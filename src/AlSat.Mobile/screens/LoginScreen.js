import React, { Component } from "react";
import { Text, KeyboardAvoidingView, StyleSheet, Button, TextInput } from "react-native";
import Constants from "expo-constants"
import Colors from "../constants/Colors";
import { ScrollView } from "react-native-gesture-handler";
import { Config } from '../Config';

console.log(Config);
console.log(Config.url.API_URL);

export default class Login extends Component {
	constructor() {
		super();

		this.state = {
			phone: ""
		}
	}

	// submiLogin() {
	// 	fetch('https://reactnative.dev/movies.json')
	// 		.then((response) => response.json())
	// 		.then((responseJson) => {
	// 			return responseJson.movies;
	// 		})
	// 		.catch((error) => {
	// 			console.error(error);
	// 		});
	// }

	render() {
		console.log(Config);
		console.log(Config.url.API_URL);

		return (
			<KeyboardAvoidingView style={styles.wrapper} behavior="padding">
				<ScrollView>
					<Text>Test</Text>
					<Text style={styles.loginHeader}>Login</Text>

					<Text style={styles.textHeader}>Phone Number</Text>
					<TextInput style={styles.textInput} autoCorrect={false} keyboardType="number-pad" />

					<Text style={styles.textHeader}>Password</Text>
					<TextInput style={styles.textInput} autoCorrect={false} secureTextEntry={true} keyboardType="number-pad" />

					<Button title="Login" />

					{/* <Button title="Login" onPress={() => submitLogin()}></Button> */}
				</ScrollView>
			</KeyboardAvoidingView>
		);
	}
}

const styles = StyleSheet.create({
	wrapper: {
		display: "flex",
		flex: 1,
		backgroundColor: "lightskyblue",
		padding: 20,
		paddingTop: Constants.statusBarHeight
	},
	loginHeader: {
		fontSize: 28,
		color: "white"
	},
	textHeader: {
		backgroundColor: "green",
		padding: 5
	},
	textInput: {
		color: "white",
		backgroundColor: "orange",
		borderBottomWidth: 1,
		padding: 10
	},
});
