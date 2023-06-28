import React, { useState } from "react";
import { useAuth } from "../../../custom/useAuth";
import { useNavigate } from "react-router";
import { ROUTES } from "../../../constants/routes";
import { FORMS } from "../../../constants/forms";
import { HelperFunctions } from "../../../util/helperFunctions";
import Button from "../../Helper/Button/Button";
import AuthService from "../../../api/AuthService";
import "../Form.css";
import "./LoginForm.css";

const LoginForm = () => {
	const loginForm = FORMS.LOGIN_FORM;
	const [loginData, setLoginData] = useState({
		email: "",
		password: "",
	});
	const [isLoading, setIsLoading] = useState(false);
	const [errorMessage, setErrorMessage] = useState("");
	const { login } = useAuth();
	const navigate = useNavigate();

	const noEmptyFields = () => {
		if (!HelperFunctions.noEmptyFields(loginData)) {
			setErrorMessage("Fields can't be empty");
			return false;
		} else {
			setErrorMessage("");
			return true;
		}
	};

	const handleLogin = async () => {
		if (noEmptyFields()) {
			setIsLoading(true);
			await AuthService.login(loginData)
				.then((response) => login(response))
				.catch((errorMessage) => setErrorMessage(errorMessage))
				.finally(() => setIsLoading(false));
		}
	};

	return (
		<form action="POST" className="form">
			<h1 className="form-title">Sign In</h1>
			<p className="form-desc">Stay updated with the newest posts</p>
			{loginForm.formGroups.map((group) => (
				<div key={group.id} className="form-group">
					{group.icon}
					<input
						id={group.id}
						type={group.type}
						className="input"
						placeholder={group.placeholder}
						onChange={(e) => setLoginData({ ...loginData, [`${group.id}`]: e.currentTarget.value })}
					/>
				</div>
			))}

			<div className="form__actions">
				<Button onButtonClick={() => handleLogin()} text="Sign In" loading={isLoading} />
			</div>
			<p className="link" onClick={() => navigate(ROUTES.REGISTER)}>
				Haven't registered yet? Click here to create an account
			</p>

			{errorMessage && <p className="error">{errorMessage}</p>}
		</form>
	);
};

export default LoginForm;
