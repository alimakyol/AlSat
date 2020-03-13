const prod = {
	url: {
		API_URL: "https://alsat.com"
	}
};

const dev = {
	url: {
		API_URL: "http://localhost:44380"
	}
};

export const Config = process.env.NODE_ENV === "development" ? dev : prod;
