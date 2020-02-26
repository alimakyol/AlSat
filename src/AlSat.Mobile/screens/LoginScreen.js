import React, { Component } from "react";
import { View, Text, KeyboardAvoidingView, ScrollView, StyleSheet } from "react-native";
import Colors from "../constants/Colors";
import { TextInput } from "react-native-gesture-handler";

export default class Login extends Component {
	render() {
		return (
			<KeyboardAvoidingView style={styles.wrapper} behavior="padding">
			<View style={styles.scrollViewWrapper}>
				<ScrollView style={styles.scrollView}>
					<Text style={styles.loginHeader}>Login</Text>
					<Text style={styles.textHeader}>Email</Text>
					<TextInput style={styles.textInput} autoCorrect={false} />
					<Text style={styles.textHeader}>Password</Text>
					<TextInput style={styles.textInput} autoCorrect={false} secureTextEntry={true} />
					<TextInput secureTextEntry={true}></TextInput>
				</ScrollView>
			</View>
			</KeyboardAvoidingView>
		);
	}
}

const styles = StyleSheet.create({
	wrapper: {
		display: "flex",
		flex: 1,
		backgroundColor: "lightskyblue"
	},
	scrollViewWrapper: {
		margin: 70,
		flex:1
	},
	scrollView: {
		paddingLeft:30,
		paddingRight:30,
		padding: 20,
		flex:1
	},
	loginHeader: {
		fontSize:28,
		color: "white"
	},
	textHeader: {

	},
	textInput: {
		color: "white",
		borderBottomWidth: 1,
		
	}

});