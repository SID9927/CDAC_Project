package com.marketplace.Service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.FileSystemResource;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Service;

import jakarta.mail.MessagingException;
import jakarta.mail.internet.MimeMessage;
import java.io.File;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@Service
public class EmailSenderService {
	private static final Logger logger = LoggerFactory.getLogger(EmailSenderService.class);

	@Autowired
	private JavaMailSender mailSender;

	public void sendSimpleEmail(String toEmail, String body, String subject) {
		try {
			SimpleMailMessage message = new SimpleMailMessage();
			message.setFrom("dsid952@gmail.com");
			message.setTo(toEmail);
			message.setText(body);
			message.setSubject(subject);
			mailSender.send(message);
			logger.info("Simple email sent successfully to {}", toEmail);
		} catch (Exception e) {
			logger.error("Failed to send simple email to {}: {}", toEmail, e.getMessage());
			throw new RuntimeException("Failed to send simple email", e);
		}
	}

	public void sendEmailWithAttachment(String toEmail, String body, String subject, String attachment)
			throws MessagingException {
		try {
			MimeMessage mimeMessage = mailSender.createMimeMessage();
			MimeMessageHelper mimeMessageHelper = new MimeMessageHelper(mimeMessage, true);

			mimeMessageHelper.setFrom("dsid952@gmail.com");
			mimeMessageHelper.setTo(toEmail);
			mimeMessageHelper.setText(body);
			mimeMessageHelper.setSubject(subject);

			FileSystemResource fileSystem = new FileSystemResource(new File(attachment));
			mimeMessageHelper.addAttachment(fileSystem.getFilename(), fileSystem);

			mailSender.send(mimeMessage);
			logger.info("Email with attachment sent successfully to {}", toEmail);
		} catch (MessagingException e) {
			logger.error("Failed to send email with attachment to {}: {}", toEmail, e.getMessage());
			throw new MessagingException("Failed to send email with attachment", e);
		}
	}
}
