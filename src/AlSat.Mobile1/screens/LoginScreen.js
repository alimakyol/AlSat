import React, { Component, useState } from 'react';
import { Text, KeyboardAvoidingView, StyleSheet, Button, TextInput } from 'react-native';
import Constants from 'expo-constants'
import Colors from '../constants/Colors';
import { ScrollView } from 'react-native-gesture-handler';
import { Config } from '../Config';

console.log(Config);
// console.log(Config.url.API_URL);

export default class Login extends Component {
	constructor() {
		super();

		this.state = {
			phone: '',
			password: ''
		}
	}

	// login = () => {
	// 	console.log(this);
	// }

	// submiLogin() {
	// 	fetch('{Config.url.API_URL}/account/login')
	// 		.then((response) => response.json())
	// 		.then((data) => {
	// 			return data.token;
	// 		})
	// 		.catch((error) => {
	// 			console.error(error);
	// 		});
	// }

	render() {
		const [phone, setPhone] = useState('');
		const [password, setPassword] = useState('');

		return (
			<KeyboardAvoidingView style={styles.wrapper} behavior='padding'>
				<ScrollView>
					{/* <Text>Test</Text> */}
					<Text style={styles.loginHeader}>Login</Text>
					{/*
					<Text style={styles.textHeader}>Phone Number</Text>
					<TextInput style={styles.textInput} autoCorrect={false} keyboardType='number-pad'
						placeholder="Password" placeholder="Phone"
						onChangeText={text => this.setState({ phone: text })}
					/>

					<Text style={styles.textHeader}>Password</Text>
					<TextInput style={styles.textInput} autoCorrect={false} secureTextEntry={true}
						keyboardType='number-pad' placeholder="Password"
						onChangeText={text => this.setState({ password: text })}
					/>

					<Button title='Login' /> */}

					{/* <Button title='Login' onPress={() => submitLogin()}></Button> */}
				</ScrollView>
			</KeyboardAvoidingView>
		);
	}
}

const styles = StyleSheet.create({
	wrapper: {
		display: 'flex',
		flex: 1,
		backgroundColor: 'lightskyblue',
		padding: 20,
		paddingTop: Constants.statusBarHeight
	},
	loginHeader: {
		fontSize: 28,
		color: 'white'
	},
	textHeader: {
		backgroundColor: 'green',
		padding: 5
	},
	textInput: {
		color: 'white',
		backgroundColor: 'orange',
		borderBottomWidth: 1,
		padding: 10
	},
});
